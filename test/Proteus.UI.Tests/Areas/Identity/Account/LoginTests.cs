
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels.Identity.Account;
using Proteus.UI.Areas.Identity.Pages.Account;
using System.Web.Mvc;
using Xunit;

namespace Proteus.UI.Tests.Areas.Identity.Account
{
    public class LoginTests : LoginTestFixture
    {
       
        public LoginTests()
        {
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login_Constructor()
        {
            //assemble and set up what each object requires
            //handled in constructor

            //act
            var result = new LoginModel();

            //assert
            Assert.IsType<LoginModel>(result);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login_OnGet_GivenNoReturnUrl_ReturnsPage()
        {
            //assemble and set up what each object requires
            var page = new LoginModel();

            //act
           var result = page.OnGet();

            //assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login_OnPostAsync_CheckboxRedirectsToLogin2Page()
        {
            //assemble and set up what each object requires
            var page = new LoginModel();
           
            page.Input = new LoginViewModel();
            page.Input.DODAccept = true;

            //act
            var result =  page.OnPost(string.Empty);

            //assert
            Assert.IsType<RedirectToPageResult>(result);
            RedirectToPageResult redirect = result as RedirectToPageResult; //<--cast here to get the actual object
            Assert.Equal("./login2", redirect.PageName);
        }
    }
}
