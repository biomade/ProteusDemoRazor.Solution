using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Proteus.Infrastructure.Data;
using Proteus.UI.Pages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Proteus.UI.Tests
{
    public class IndexPageTests
    {
        private IndexModel _pageModel;

        [SetUp]
        public void Setup(ILogger<IndexModel> logger)
        {
            // Arrange
            _pageModel = new IndexModel(logger);
        }

        [Test]
        public void Index_Page_Test()
        {
            
            //act
            var result = _pageModel.OnGet();
            // Assert
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
      
}
