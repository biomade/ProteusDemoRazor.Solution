using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proteus.Infrastructure.Identity;
using Proteus.Infrastructure.Repository.Base;
using Proteus.Infrastructure.Data;
using Proteus.UI.HealthCheck;
using Proteus.Infrastructure.Repository;
using Proteus.Application.Interfaces;
using Proteus.Application.Services;
using Proteus.Core.Interfaces.Repositories.Base;
using Proteus.Core.Interfaces.Repositories;
using SmartBreadcrumbs.Extensions;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity.Stores;
using Proteus.Core.Interfaces.Identity;
using Microsoft.AspNetCore.Http;

namespace Proteus.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        private readonly IHostEnvironment _env;
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //https://pradeeploganathan.com/aspnetcore/https-in-asp-net-core-31/
            //https://www.thesslstore.com/blog/how-to-make-ssl-certificates-play-nice-with-asp-net-core/
            if (!_env.IsDevelopment())
            {
                services.AddHttpsRedirection(opts =>
                {
                    opts.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    opts.HttpsPort = 44300;
                });
            }
            else
            {
                services.AddHttpsRedirection(opts =>
                {
                    opts.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                    opts.HttpsPort = 44300;
                });
            }

            /**
                Create a service for DI that will return the ApplicationConfiguration
                section of appsettings. This is just a factory function.
            */
            services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>(
                e => Configuration.GetSection("AppSettings")
                        .Get<ApplicationConfiguration>());

            ConfigureDatabases(services);

            //TODO IDENTITY: Step 5a - Configure the services for Identity
            //register the IdentityContext as a service
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            //tell Identity to use our user and roles
            services.AddIdentity<User, Role>(
            );

            services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, Role>>();

            //use our custom storage providers
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._@";
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "ProteusDemo";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(Configuration["AppSettings:SessionTimeOutMinutes"])); //how long to keep the cookie, then the user will be logged out
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/AccessDenied";
            });
            //end todo

            services.AddBreadcrumbs(GetType().Assembly, options =>
            {
                options.TagClasses = "col-sm-6";
                options.OlClasses = "breadcrumb float-sm-right";
                options.LiClasses = "breadcrumb-item";
                options.ActiveLiClasses = "breadcrumb-item active";
                //// Testing
                //options.DontLookForDefaultNode = true;
            });
            // aspnetrun dependencies
            RegisterServices(services);

            //https://docs.microsoft.com/en-us/aspnet/core/security/authorization/razor-pages-authorization?view=aspnetcore-3.1
            services.AddRazorPages()
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizePage("/Index");
                options.Conventions.AuthorizePage("/Privacy");
                options.Conventions.AuthorizeFolder("/Category");
                options.Conventions.AuthorizeFolder("/Product");
                options.Conventions.AuthorizeAreaFolder("StyleGuide", "/");//all pages in the style guide area
                options.Conventions.AuthorizeAreaPage("Identity", "/Accounts/Manage");
                options.Conventions.AuthorizeAreaFolder("Identity", "/Roles");
                options.Conventions.AuthorizeAreaFolder("Identity", "/Users");
                options.Conventions.AuthorizeAreaFolder("Identity", "/UserRoles");
                //options.Conventions.AllowAnonymousToPage("/Private/PublicPage");
                //options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");
            });

            //TODO CAC Authentication 0: Add Microsoft.AspNetCoreAuthentication.Certificate nuget package
            //TODO CAC Authentication 1: add "https_port": 443, to the appsettings.json config
            //TODO CAC Authentication 2a: configure service to validate cert         
            //TODO CAC Authentication 2b: configure authentication Serive

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //TODO IDENTITY: Step 5b to seed data, modify the interface of this method
        //Was - public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            //TODO CAC Authentication 3:Add use cert forwarding
            app.UseCertificateForwarding();
            //TODO IDENTITY: Step 5c Add use of Authentication
            app.UseAuthentication();

            //TODO IDENTITY: Step 5d - call to method for seed data
            IdentityDbContextSeed.SeedData(userManager, roleManager);
            //TODO IDENTITY: Step 5e - turn on authorization for users and roles
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
        private void RegisterServices(IServiceCollection services)
        {
            // Add Core Layer
            // Add Infrastructure Layer
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRoleStore, UserRoleStore>();

            // Add Application Layer
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICertValidationService, CertValidationService>();

            // Add Web Layer

            // Add Miscellaneous
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                .AddCheck<IndexPageHealthCheck>("home_page_health_check");
        }

        private void ConfigureDatabases(IServiceCollection services)
        {
            //// use in-memory database
            //services.AddDbContext<ProteusContext>(options =>
            //    options.UseInMemoryDatabase("Proteus"));
            // Add Identity DbContext
            //services.AddDbContext<IdentityDbContext>(options =>
            //    options.UseInMemoryDatabase("Identity"));


            // use real database
            //NOTE IF YOU ARE MODIFYING THE DATABASE RUN
            //Add-Migration  xxx -Context ProteusContext
            //if used execute the following commands in the Package Manager Console
            //1) dotnet restore
            //2a) update-database -Context ProteusContext
            //2b) update-database -Context AppIdentityDbContext
            services.AddDbContext<ProteusContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ProteusConnection")));
            // Add Identity DbContext
            services.AddDbContext<IdentityDbContext>(c =>
            c.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
        }
    }
}
