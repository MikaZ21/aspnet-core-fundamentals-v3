using System;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrm.Web.ViewComponent
{
    public class GreetingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return ViewComponent("Default", "Hello");
        }
    }
}

