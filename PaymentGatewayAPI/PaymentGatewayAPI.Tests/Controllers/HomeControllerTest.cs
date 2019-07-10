using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGatewayAPI;
using PaymentGatewayAPI.Controllers;

namespace PaymentGatewayAPI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestHomePage()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
