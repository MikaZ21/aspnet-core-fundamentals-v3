using System;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrm.Web.Controllers
{
	[Route("about")]
	public class AboutController
	{
        //[Route("{id:regex(^UPR)}")]
        [Route("")]
		[Route("phone")]
		[Route("{phone:regex(^\\d{{3}}-\\d{{3}}-\\d{{4}}$)}")]
		public string Phone(string phone)
		{
            return "Phone number from 'AboutController~!'" + phone;
        }

        // https://localhost:7059/about/address?country=Japan
		[Route("address")]
        public string Address([FromQuery]string country)
		{
			return "Country " + country + " from 'AboutController.'";
		}
	}
}

