using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrm.Web.ViewComponents

{
	public class LoginLogoutViewComponent : ViewComponent
	{
        private readonly UserManager<CrmUser> userManager;

        public LoginLogoutViewComponent(UserManager<CrmUser> userManager)
        {
            this.userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
		{
            if (User.Identity.IsAuthenticated) { 
                var user = await this.userManager.FindByEmailAsync(this.User.Identity.Name);
                return View(user);
            }
            else
            {
                return View(new CrmUser());
            }
        }
	}
}