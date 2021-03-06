using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proteus.Infrastructure.Identity;
using Proteus.Infrastructure.Repository.Base;
using Proteus.Infrastructure.Data;
using AutoMapper;
using Proteus.UI.HealthCheck;
using Proteus.Infrastructure.Repository;
using Proteus.Application.Interfaces;
using Proteus.Application.Services;
using Proteus.Core.Interfaces.Repositories.Base;
using Proteus.Core.Interfaces.Repositories;

namespace Proteus.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabases(services);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                       .AddDefaultUI()
                       .AddEntityFrameworkStores<AppIdentityDbContext>()
                                       .AddDefaultTokenProviders();

            // aspnetrun dependencies
            RegisterServices(services);

            services.AddRazorPages();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseAuthentication();
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

            // Add Application Layer
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

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
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseInMemoryDatabase("Identity"));


            // use real database
            //if used execute the following commands in the Package Manager Console
            //1) dotnet restore
            //2a) update-database -Context ProteusContext
            //2b) update-database -Context AppIdentityDbContext
            services.AddDbContext<ProteusContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ProteusConnection")));
            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(c =>
            c.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
        }
    }
}
