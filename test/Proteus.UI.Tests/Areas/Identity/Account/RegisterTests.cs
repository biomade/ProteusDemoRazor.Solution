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
using Proteus.Application.ViewModels.Identity.Account;
using RegisterModel = Proteus.UI.Areas.Identity.Pages.Account.RegisterModel;

namespace Proteus.UI.Tests.Areas.Identity.Account
{
    public class RegisterTests: RegisterTestFixture
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly Mock<ILogger<RegisterModel>> _logger;
        private readonly Mock<IApplicationConfiguration> _configuration;

        public RegisterTests()
        {
            _signInManager = SignInManager; // new Mock<FakeSignInManager>();
            _userManager = UserManager; // new Mock<FakeSignInManager>();
            _logger = new Mock<ILogger<RegisterModel>>();
            _configuration = new Mock<IApplicationConfiguration>();
            #region config
            _configuration.Setup(s => s.DefaultRole).Returns("Vistor");
            #endregion
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Register_Constructor()
        {
            //assemble and set up what each object requires
            //part of constructor

            //act
            var result = new RegisterModel(_userManager, _signInManager, _logger.Object, _configuration.Object);

            //assert
            Assert.IsType<RegisterModel>(result);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Register_OnGet_GivenNoReturnUrl_ReturnsReturnPageWithEmptyReturnURLProperty()
        {
            //assemble and set up what each object requires
            var pageModel = GetPageModel();

            //act
            var result = pageModel.OnGet();

            //assert
            Assert.IsType<PageResult>(result);
            Assert.Null(pageModel.ReturnUrl);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Register_OnGet_GivenReturnUrl_ReturnsReturnPageWithReturnURLProperty()
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
        public void Register_OnPostAsync_InValidModelStateReturnsPage()
        {
            //assemble and set up what each object requires
            var pageModel = GetPageModel();
            //    //for invalid Model state
            pageModel.ModelState.AddModelError("Message.Text", "The Text field is required.");

            //act
            var result = pageModel.OnPostAsync(string.Empty).Result;

            //assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Register_OnPostAsync_ValidModel_UserCreatedSuccess()
        {
            var pageModel = GetPageModel();
            //valid Model state
            pageModel.Input = new RegisterViewModel()
            {
                Name = "mary.lamb",                
                Email = "mary.lamb@gmail.com",
                Phone = "800-555-1212",
                GovPOCPhoneNumber = "800-555-1212",
                GovPOCName = "Joe B",
                GovPOCEmail = "joe.b@gmail.com" ,
                Password = "Abc123!"
            };

            //act
            var result = pageModel.OnPostAsync(string.Empty).Result;

            //assert
            Assert.IsType<RedirectToPageResult>(result);
            RedirectToPageResult redirect = result as RedirectToPageResult; //<--cast here to get the actual object
            Assert.Equal("./AccountDisabled", redirect.PageName);
        }

        private RegisterModel GetPageModel()
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

            var pageModel = new RegisterModel( _userManager,_signInManager, _logger.Object, _configuration.Object)
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };
            return pageModel;
        }
    }
}
