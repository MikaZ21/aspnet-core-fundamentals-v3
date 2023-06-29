using System;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrm.Web.Controllers
{
	[Route("contact")]
	public class ContactController
	{
        [Route("ph")]
        public string Phone()
		{
			return "111-222-3333";
		}

		// Or [Route("")] or blanck to show name just /contact
		[Route("nm")]
		public string Name()
		{
			return "Mika";
		}
	}
}

