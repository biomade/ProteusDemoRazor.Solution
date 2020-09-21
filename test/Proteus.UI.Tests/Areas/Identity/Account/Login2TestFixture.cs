using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Proteus.Application.Interfaces;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;

namespace Proteus.UI.Tests.Areas.Identity.Account
{
    public class Login2TestFixture
    {
        public readonly UserManager<User> UserManager;
        public readonly SignInManager<User> SignInManager;
        public readonly IdentityDbContext DbContext;
       

        public Login2TestFixture()
        {
            #region userMgr
            var options = CreateNewContextOptions();
            IdentityDbContext dbContext = new IdentityDbContext(options);

            var users = new List<User>
            {
                new User()
                {
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                    UserName = "AdminIsLockedOut",
                    NormalizedUserName ="Admin".ToUpper(),
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    IsEnabled = true,
                    IsLockedOut = true,
                    CreatedDate = System.DateTime.Now,
                    LastLoginDate = System.DateTime.Now,
                    EDI = "NONE",
                },
                new User()
                {
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                    UserName = "AdminIsNotEnabled",
                    NormalizedUserName = "Admin".ToUpper(),
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    IsEnabled = false,
                    IsLockedOut = false,
                    CreatedDate = System.DateTime.Now,
                    LastLoginDate = System.DateTime.Now,
                    EDI = "NONE",
                },
                new User()
                {
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                    UserName = "AdminPastLogInAmount",
                    NormalizedUserName = "Admin".ToUpper(),
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    IsEnabled = true,
                    IsLockedOut = false,
                    CreatedDate =  new System.DateTime(2020,1,1),
                    LastLoginDate = new System.DateTime(2020,1,1),
                    EDI = "NONE",
                },
                new User()
                {
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                    UserName = "AdminAlreadyOnLine",
                    NormalizedUserName = "Admin".ToUpper(),
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    IsEnabled = true,
                    IsLockedOut = false,
                    CreatedDate =  System.DateTime.Now,
                    LastLoginDate =System.DateTime.Now,
                    UserOnLine = true,
                    EDI = "NONE",
                },
                new User()
                {
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                    UserName = "Admin",
                    NormalizedUserName = "Admin".ToUpper(),
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    IsEnabled = true,
                    IsLockedOut = false,
                    CreatedDate =  System.DateTime.Now,
                    LastLoginDate =System.DateTime.Now,
                    UserOnLine = false,
                    EDI = "NONE",
                }
            }.AsQueryable();

            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.Users)
                .Returns(users);

            fakeUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            fakeUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            fakeUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            fakeUserManager.Setup(x =>
                    x.ChangeEmailAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var userName = "AdminIsNotEnabled";
            var user = users.ToList().Where(u => u.UserName == userName).First();
            var returnedUser = fakeUserManager.Setup(x => x.FindByNameAsync(userName)).ReturnsAsync(user);

            userName = "AdminIsLockedOut";
            user = users.ToList().Where(u => u.UserName == userName).First();
            returnedUser = fakeUserManager.Setup(x => x.FindByNameAsync(userName)).ReturnsAsync(user);

            userName = "AdminPastLogInAmount";
            user = users.ToList().Where(u => u.UserName == userName).First();
            returnedUser = fakeUserManager.Setup(x => x.FindByNameAsync(userName)).ReturnsAsync(user);

            userName = "AdminAlreadyOnLine";
            user = users.ToList().Where(u => u.UserName == userName).First();
            returnedUser = fakeUserManager.Setup(x => x.FindByNameAsync(userName)).ReturnsAsync(user);

            userName = "Admin";
            user = users.ToList().Where(u => u.UserName == userName).First();
            returnedUser = fakeUserManager.Setup(x => x.FindByNameAsync(userName)).ReturnsAsync(user);
            var result = fakeUserManager.Setup(x => x.CheckPasswordAsync(user, "Abc123")).ReturnsAsync(false);
            result = fakeUserManager.Setup(x => x.CheckPasswordAsync(user, "Abc123!")).ReturnsAsync(true);
            #endregion

            #region SignInMgr
            var signInManager = new Mock<FakeSignInManager>();
            //signInManager.Setup(
            //        x => x.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(),
            //            It.IsAny<bool>()))
            //    .ReturnsAsync(SignInResult.Success);

            signInManager.Setup(x => x.PasswordSignInAsync("Admin", "Abc123!",false, false)).ReturnsAsync(SignInResult.Success);

            UserManager = fakeUserManager.Object;
            SignInManager = signInManager.Object;
            #endregion
        }

        public void Dispose()
        {
            UserManager?.Dispose();
            DbContext?.Dispose();
        }

        public DbContextOptions<IdentityDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<IdentityDbContext>();
            builder.UseInMemoryDatabase("Identity")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }


    public class FakeUserManager : UserManager<User>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object)
        {
        }
    }

    public class FakeSignInManager : SignInManager<User>
    {
        public FakeSignInManager() : base(new Mock<FakeUserManager>().Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object)
        {
        }
    }


    public class FakeConfiguration{
        private IConfigurationRoot GetConfigurationRoot()
        {
            string pathToFile = @"C:\Users\lauri\Repos\ProteusDemoRazor.Solution\src\ProteusDemoRazor.UI\";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(pathToFile)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            return configuration;
        }
    }

}
