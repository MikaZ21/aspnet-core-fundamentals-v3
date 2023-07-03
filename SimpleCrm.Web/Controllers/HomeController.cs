using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models;

namespace SimpleCrm.Web.Controllers
{
	public class HomeController : Controller 
	{
		private ICustomerData _customerData;
		private readonly IGreeter _greeter;

		public HomeController(ICustomerData customerData, IGreeter greeter)
		{
			_customerData = customerData;
			_greeter = greeter;
		}

		public IActionResult Details(int id)
		{
			//A way to get a single customer.
			//var cust = _customerData.GetAll().FirstOrDefault((x) => x.Id == id);
			Customer cust = _customerData.Get(id);
			return View(cust);
		}

		public IActionResult Index()
		{
			var model = new HomePageViewModel();
			model.Customers = _customerData.GetAll();
			model.CurrentMessage = _greeter.GetGreeting();
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
