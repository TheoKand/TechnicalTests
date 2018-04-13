using System;
using System.Web.Mvc;
using Moq;
using MvcTest.WebApplication.Controllers;
using MvcTest.WebApplication.Models;
using MvcTest.WebApplication.Services;
using NUnit.Framework;
using Shouldly;

namespace MvcTest.UnitTests {
	[TestFixture]
	public class HomeControllerTests {

		private Mock<IClock> clock;
		[SetUp]
		public void SetUp() {
			clock = new Mock<IClock>();
		}

		[Test]
		public void Index_Returns_View() {
			var c = new HomeController(clock.Object);
			var result = c.Index();
			result.ShouldBeTypeOf<ViewResult>();
		}

		[Test]
		public void Welcome_Returns_View() {
			var controller = new HomeController(clock.Object);
			var result = controller.Welcome("Eddie") as ViewResult;
			result.ShouldBeTypeOf<ViewResult>();
		}

		[Test]
		public void Welcome_Includes_Name() {
			var controller = new HomeController(clock.Object);
			var result = controller.Welcome("Eddie") as ViewResult;
			var model = result.ViewData.Model as WelcomeViewData;
			model.ShouldBeTypeOf<WelcomeViewData>();
			model.Name.ShouldBe("Eddie");
		}

		[Test]
		public void Welcome_Formats_Time_Correctly() {
			clock.Setup(c => c.UtcNow).Returns(new DateTime(2001, 2, 3, 4, 5, 6));
			var controller = new HomeController(clock.Object);
			var result = controller.Welcome("Eddie") as ViewResult;
			var model = result.ViewData.Model as WelcomeViewData;
			model.Date.ShouldBe("February 3, 2001");
		}
	}
}
