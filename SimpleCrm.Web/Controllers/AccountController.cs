using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models.Account;

namespace SimpleCrm.Web.Controllers
{

	public class AccountController : Controller
	{
        private readonly UserManager<CrmUser> userManager;
        private readonly SignInManager<CrmUser> signInManager;

        public AccountController(UserManager<CrmUser> userManager, SignInManager<CrmUser> signInManager)
        {
            this.userManager = userManager;
			this.signInManager = signInManager;
        }


        [HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new CrmUser
				{
					UserName = model.UserName,
					DisplayName = model.DisplayName,
					Email = model.UserName
				};
				var createResult = await this.userManager.CreateAsync(user, model.Password);
				if (createResult.Succeeded)
				{
					await this.signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				foreach (var result in createResult.Errors)
				{
					ModelState.AddModelError("", result.Description);
				}
			}
			return View();
		}

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginUserViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var loginResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
				
					if (loginResult.Succeeded)
					{
						if (Url.IsLocalUrl(returnUrl))
						{
							return Redirect(returnUrl);
						}
						else
						{
							return RedirectToAction("Index", "Home");
						}
					}
				
				if (!loginResult.Succeeded)
				{
					ModelState.AddModelError("", "Could not login");
				}
			}
            return View();
        }

        [HttpGet]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}