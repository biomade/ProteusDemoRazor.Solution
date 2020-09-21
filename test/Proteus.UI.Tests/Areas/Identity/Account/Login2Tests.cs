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
using Proteus.UI.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Proteus.Application.ViewModels.Identity.Account;

namespace Proteus.UI.Tests.Areas.Identity.Account
{
    public class Login2Tests : Login2TestFixture
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly Mock<ILogger<Login2Model>> _logger;
        private readonly Mock<IApplicationConfiguration> _configuration;

        public Login2Tests()
        {
            _signInManager = SignInManager; // new Mock<FakeSignInManager>();
            _userManager = UserManager; // new Mock<FakeSignInManager>();
            _logger = new Mock<ILogger<Login2Model>>();
            _configuration = new Mock<IApplicationConfiguration>();
            #region config
            _configuration.Setup(s => s.MaxDaysBetweenLogins).Returns(35);
           #endregion
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login2_Constructor()
        {
            //assemble and set up what each object requires
            //part of constructor

            //act
            var result = new Login2Model(_signInManager, _userManager, _logger.Object, _configuration.Object);

            //assert
            Assert.IsType<Login2Model>(result);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login_OnGet_GivenNoReturnUrl_ReturnsReturnPageWithEmptyReturnURLProperty()
        {
            //assemble and set up what each object requires
            var pageModel = GetPageModel();

            //act
            var result = pageModel.OnGet();

            //assert
            Assert.IsType<PageResult>(result);
            Assert.Equal(string.Empty, pageModel.ReturnUrl);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login_OnGet_GivenReturnUrl_ReturnsReturnPageWithReturnURLProperty()
        {
            //setup
            var pageModel = GetPageModel();

            //act
            var result = pageModel.OnGet("AnyPage");

            //assert
            Assert.IsType<PageResult>(result);
            Assert.Equal("AnyPage", pageModel.ReturnUrl);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public async Task Login_OnPostAsync_InValidModelStateReturnsPageAsync()
        {
            //assemble and set up what each object requires
            var pageModel = GetPageModel();
            //    //for invalid Model state
            pageModel.ModelState.AddModelError("Message.Text", "The Text field is required.");

            //act
            var result = await pageModel.OnPostAsync(string.Empty);

            //assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public async Task Login_OnPostAsync_ValidModel_UserNotFound_RedirectsToRegisterPageAsync()
        {
            var pageModel = GetPageModel();
            //valid Model state
            pageModel.Input = new Login2ViewModel()
            {
                UserName = "MaryLamb",
                Password = "password"
            };

            //act
            var result = await pageModel.OnPostAsync(string.Empty);

            //assert
            Assert.IsType<RedirectToPageResult>(result);
            RedirectToPageResult redirect = result as RedirectToPageResult; //<--cast here to get the actual object
            Assert.Equal("./Register", redirect.PageName);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public async Task Login_OnPostAsync_ValidModel_UserFound_AccountDisabledAsync()
        {
            //assemble           
            var httpContext = new DefaultHttpContext();
            var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            var pageModel = new Login2Model(_signInManager, _userManager, _logger.Object, _configuration.Object)
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };
            //valid Model state
            pageModel.Input = new Login2ViewModel()
            {
                UserName = "AdminIsNotEnabled",
                Password = "Abc123!"
            };

            //act
            var result = await pageModel.OnPostAsync(string.Empty);

            //assert
            Assert.IsType<RedirectToPageResult>(result);
            RedirectToPageResult redirect = result as RedirectToPageResult; //<--cast here to get the actual object
            Assert.Equal("./AccountDisabled", redirect.PageName);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public async Task Login_OnPostAsync_ValidModel_UserFound_IsLockedOutAsync()
        {
            Login2Model pageModel = GetPageModel();
            //valid Model state
            pageModel.Input = new Login2ViewModel()
            {
                UserName = "AdminIsLockedOut",
                Password = "Abc123!"
            };

            //act
            var result = await pageModel.OnPostAsync(string.Empty);

            //assert
            Assert.IsType<RedirectToPageResult>(result);
            RedirectToPageResult redirect = result as RedirectToPageResult; //<--cast here to get the actual object
            Assert.Equal("./AccountLocked", redirect.PageName);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public async Task Login_OnPostAsync_ValidModel_UserFound_PastLoginDurationAsync()
        {
            Login2Model pageModel = GetPageModel();
            //valid Model state
            pageModel.Input = new Login2ViewModel()
            {
                UserName = "AdminPastLogInAmount",
                Password = "Abc123!"
            };

            //act
            var result = await pageModel.OnPostAsync(string.Empty);

            //assert
            Assert.IsType<RedirectToPageResult>(result);
            RedirectToPageResult redirect = result as RedirectToPageResult; //<--cast here to get the actual object
            Assert.Equal("./AccountLocked", redirect.PageName);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public async Task Login_OnPostAsync_ValidModel_UserFound_UserOnLineAsync()
        {
            Login2Model pageModel = GetPageModel();
            //valid Model state
            pageModel.Input = new Login2ViewModel()
            {
                UserName = "AdminAlreadyOnLine",
                Password = "Abc123!"
            };

            //act
            var result = await pageModel.OnPostAsync(string.Empty);

            //assert
            Assert.IsType<PageResult>(result);
            
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public async Task Login_OnPostAsync_ValidModel_UserFound_InvalidPasswordAsync()
        {
            Login2Model pageModel = GetPageModel();
            //valid Model state
            pageModel.Input = new Login2ViewModel()
            {
                UserName = "Admin",
                Password = "Abc123"
            };

            //act
            var result = await pageModel.OnPostAsync(string.Empty);

            //assert
            Assert.IsType<PageResult>(result);

        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public async Task Login_OnPostAsync_ValidModel_UserFound_ValidPasswordAsync()
        {
            Login2Model pageModel = GetPageModel();
            //valid Model state
            pageModel.Input = new Login2ViewModel()
            {
                UserName = "Admin",
                Password = "Abc123!"
            };

            //act
            var result = await pageModel.OnPostAsync(null);

            //assert
            Assert.IsType<LocalRedirectResult>(result);

        }

        private Login2Model GetPageModel()
        {

            //assemble           
            var httpContext = new DefaultHttpContext();
            var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            var pageModel = new Login2Model(_signInManager, _userManager, _logger.Object, _configuration.Object)
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };
            return pageModel;
        }
    }
}
