using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models;

namespace SimpleCrm.Web.Controllers
{
	public class HomeController : Controller 
	{
		public IActionResult Index()
		{
			var model = new CustomerModel{
			 Id = 1,
			 FirstName = "John",
			 LastName = "Doe",
			 PhoneNumber = "111-222-3333"
			};

            return new ObjectResult(model);
		}
	}
}
