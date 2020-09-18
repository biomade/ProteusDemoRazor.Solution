using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Moq;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels.Identity.Account;
using Proteus.Core.Entities.Identity;
using Proteus.UI.Areas.Identity.Pages.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proteus.UI.Tests.Areas.Identity.Account
{
     public class Login2Tests 
    {
        //private readonly Mock<FakeSignInManager> _signInManager;
        private readonly Mock<ILogger<Login2Model>> _logger;
        private readonly Mock<IApplicationConfiguration> _configuration;
        public Login2Tests()
        {
            //_signInManager = new Mock<FakeSignInManager>();
            _logger = new Mock<ILogger<Login2Model>>();
           _configuration = new Mock<IApplicationConfiguration>();
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login2_Constructor()
        {
            //assemble and set up what each object requires
            //part of constructor

            //act
            //var result = new Login2Model(_signInManager.Object, _logger.Object, _configuration.Object);

            //assert
            //Assert.IsType<Login2Model>(result);
        }
/*
        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login_OnGet_GivenNoReturnUrl_ReturnsReturnPageWithEmptyReturnURLProperty()
        {
            //assemble and set up what each object requires
            //_signInManager.Setup(s=>s.SignOutAsync()).Returns(Task.FromResult<SignOutResult>(new SignOutResult(){ }));

            var pageModel = new Login2Model(_signInManager.Object, _logger.Object, _configuration.Object);  
          
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
            var pageModel = new Login2Model(_signInManager.Object, _logger.Object, _configuration.Object);

            //act
            var result = pageModel.OnGet("AnyPage");

            //assert
            Assert.IsType<PageResult>(result);           
            Assert.Equal("AnyPage", pageModel.ReturnUrl);
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login_OnPostAsync_InValidModelStateReturnsPage()
        {
            //assemble and set up what each object requires
            var pageModel = new Login2Model(_signInManager.Object, _logger.Object, _configuration.Object);
            //    //for invalid Model state
            pageModel.ModelState.AddModelError("Message.Text", "The Text field is required.");

            //act
            var result = pageModel.OnPostAsync(string.Empty).Result;

            //assert
            Assert.IsType<PageResult>(result); 
        }

        [Fact]
        [Trait("UI", "Areas_Identity_Account")]
        public void Login_OnPostAsync_ValidModelStateReturnsPage()
        {
            // _signInManager.UserManager.FindByNameAsync(Input.UserName);
            //set up the sign in manager to return a user
            //_signInManager.Setup(s => s.UserManager.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(base.GetUser) ;
            //assemble and set up what each object requires
            var pageModel = new Login2Model(_signInManager.Object, _logger.Object, _configuration.Object);
            //setup username and password

            //valid Model state
            pageModel.Input = new Login2ViewModel()
            {
                UserName = "Admin",
                Password = "Abc123!"
            };


            //act
            var result = pageModel.OnPostAsync(string.Empty).Result;

            //assert
            Assert.IsType<PageResult>(result);
        }
    */
    }
}
