using Moq;
using NUnit.Framework;
using Proteus.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Infrastructure.Tests.Services
{
    [TestFixture]//denotes a class that contains unit tests.
    public class EmailSenderTests
    {
        //use moq to abstract the interfaces
        private readonly EmailSender _senderUnderTest;

        public EmailSenderTests()
        {
            _senderUnderTest = new EmailSender();
        }

        [Test]
        public void EmailSender_ShouldReturnTrueAfterEmailIsSent()
        {
            //Arrange -gather what is needed for the tests


            //Act call the code

            //Assert did the test pass or not
        }
    }
}
