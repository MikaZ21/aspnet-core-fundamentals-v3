using System;
namespace SimpleCrm.Web.Controllers
{
	public class HomeController
	{
		public string Index(string id)
		{
			return "Hello from a HomeController! and Id is," + id + "\ne.g. https://localhost:7059/home/index/5";
		}
	}
}

