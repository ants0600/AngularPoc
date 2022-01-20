using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainWebsite.Models;

namespace MainWebsite.Controllers
{
	public class HomeController : Controller
	{
		private readonly ITestService _testService;

		public HomeController(ITestService testService)
		{
			this._testService = testService;
		}
		public ActionResult Index()
		{
			var test = this._testService.Test();
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}