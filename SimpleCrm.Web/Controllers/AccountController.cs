using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models.Account;

namespace SimpleCrm.Web.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Register(RegisterUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var RegisterUserView = new RegisterUserViewModel
				{
					UserName = model.UserName,
					DisplayName = model.DisplayName,
					Password = model.Password,
					ConfirmPassword = model.ConfirmPassword
				};
				
			}
			return View();
		}
	}
}

