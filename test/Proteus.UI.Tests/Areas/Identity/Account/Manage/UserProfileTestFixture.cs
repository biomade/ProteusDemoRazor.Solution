using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;

namespace Proteus.UI.Tests.Areas.Identity.Account.Manage
{
    public class UserProfileTestFixture
    {
        public readonly UserManager<User> UserManagerMock;
        public readonly IdentityDbContext DbContext;


        public UserProfileTestFixture()
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
                    UserName = "Admin",
                    NormalizedUserName ="Admin".ToUpper(),
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    IsEnabled = true,
                    IsLockedOut = true,
                    CreatedDate = System.DateTime.Now,
                    LastLoginDate = System.DateTime.Now,
                    EDI = "1234567890",
                }
            }.AsQueryable();

            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.Users)
                .Returns(users);

            fakeUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            //fakeUserManager.Setup(x =>
            //        x.ChangeEmailAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
            //    .ReturnsAsync(IdentityResult.Success);


            var userName = "Admin";
            var user = users.ToList().Where(u => u.UserName == userName).First();
            var returnedUser = fakeUserManager.Setup(x => x.FindByNameAsync(userName)).ReturnsAsync(user);

            
            UserManagerMock = fakeUserManager.Object;
           
            #endregion
        }

        public void Dispose()
        {
            UserManagerMock?.Dispose();
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


}
