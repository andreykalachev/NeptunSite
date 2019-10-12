using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptun.Controllers;
using Neptun.Models.Email;

namespace NeptunTests
{
    [TestClass]
    public class FeedBackControllerTest
    {
        [TestMethod]
        public async Task EmailSendTest()
        {
            //arrange
            var feedBackController = new FeedBacksController();
            var email = new EmailRespond("andrey.kalachev1478@gmail.com", "Messi2012", "andrey.kalachev341478od@gmail.com", "Header", "Test",
                "This is a test email");
            //act
            var result = await feedBackController.SendEmail(email);
            //assert
            Assert.IsNotNull(result);
        }
    }
}
