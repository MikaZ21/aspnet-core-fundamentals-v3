using System;
namespace SimpleCrm.Web.Models
{
	public class HomePageViewModel
	{
		//public string CurrentMessage { get; set; }
		public IEnumerable<Customer> Customers { get; set; }
    }
}

