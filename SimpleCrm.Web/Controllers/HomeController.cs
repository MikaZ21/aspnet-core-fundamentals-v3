using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models;

namespace SimpleCrm.Web.Controllers
{
	public class HomeController : Controller 
	{
		private ICustomerData _customerData;
		public HomeController(ICustomerData customerData)
		{
			_customerData = customerData;
		}

		public IActionResult Index()
		{
			var model = _customerData.GetAll();
			//var model = new CustomerModel
			//{
			// Id = 1,
			// FirstName = "Mika",
			// LastName = "Zukeyama",
			// PhoneNumber = "111-222-3333",
			// Email = "aaa@gmail.com",
			// FavoriteSeason = "Spring",
			// FavoriteFlower = "Hydrangea"
			//};

            //return View("Home",model);
            return View(model);
        }
    }
}
