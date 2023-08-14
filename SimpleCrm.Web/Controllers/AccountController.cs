using System;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrm.Web.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
	}
}

