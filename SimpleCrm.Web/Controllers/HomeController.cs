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
			if(cust == null)
			{
				return RedirectToAction(nameof(Index));
			}
			return View(cust);
		}

		[HttpGet()]
		public IActionResult Edit(int id)
		{
			var customer = _customerData.Get(id);
			if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }
			var model = new CustomerEditViewModel
			{
				Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                OptInNewsletter = customer.OptInNewsletter,
                Type = customer.Type
            };
			return View(model);
		}

		[HttpPost()]
		[ValidateAntiForgeryToken()]
		public IActionResult Edit(CustomerEditViewModel model)
		{
			if (ModelState.IsValid)
			{
                var customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    OptInNewsletter = model.OptInNewsletter,
                    Type = model.Type
                };
                _customerData.Save(customer);

                return RedirectToAction(nameof(Details), new { id = customer.Id });
			}
			return View();
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

		[HttpGet()]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost()]
		[ValidateAntiForgeryToken()]
		public IActionResult Create(CustomerEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				var customer = new Customer
				{
					FirstName = model.FirstName,
					LastName = model.LastName,
					PhoneNumber = model.PhoneNumber,
					OptInNewsletter = model.OptInNewsletter,
					Type = model.Type
				};
				_customerData.Save(customer);

				return RedirectToAction(nameof(Details), new { id = customer.Id });
			}
			return View();
		}		
    }
}
