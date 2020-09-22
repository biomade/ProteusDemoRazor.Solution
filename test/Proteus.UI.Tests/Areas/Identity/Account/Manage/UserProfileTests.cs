using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;
using Proteus.Application.Interfaces;
using Proteus.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Proteus.UI.Areas.Identity.Pages.Account.Manage;
using Proteus.Application.ViewModels.Identity.Account.Manage;
using System.Security.Principal;
using System.Security.Claims;

namespace Proteus.UI.Tests.Areas.Identity.Account.Manage
{
    public class UserProfileTests : UserProfileTestFixture
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly Mock<ILogger<UserProfileModel>> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserProfileTests()
        {
            _userManager = UserManagerMock; // new Mock<FakeSignInManager>();
            _logger = new Mock<ILogger<UserProfileModel>>();
            _passwordHasher = new PasswordHasher<User>();

        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account_Manage")]
        public void UserProfile_Constructor()
        {
            //assemble and set up what each object requires
            //part of constructor

            //act
            var result = new UserProfileModel(_userManager, _logger.Object, _passwordHasher);

            //assert
            Assert.IsType<UserProfileModel>(result);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account_Manage")]
        public async Task UserProfile_OnGetAsync_ReturnsViewModel()
        {
            //assemble and set up what each object requires
            var pageModel = GetPageModel();

            //act
            var result = await pageModel.OnGet();

            //assert
            Assert.IsType<UserProfileViewModel>(pageModel.Input);
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account_Manage")]
        public async Task UserProfile_OnPostAsync_InvalidModelAsync()
        {
            var pageModel = GetPageModel();
            pageModel.ModelState.AddModelError("Message.Text", "The Text field is required.");

            //act
            var result = await pageModel.OnPostAsync();

            //assert
            Assert.IsType<PageResult>(result);
        }

        private UserProfileModel GetPageModel()
        {
            //assemble           
            //var httpContext = new DefaultHttpContext();
            var displayName = "Admin";
            var identity = new GenericIdentity(displayName);
            var principle = new ClaimsPrincipal(identity);
            // use default context with user
            var httpContext = new DefaultHttpContext()
            {
                User = principle
            };
            var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            var pageModel = new UserProfileModel(_userManager, _logger.Object, _passwordHasher)
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };
            return pageModel;
        }
    }
}
