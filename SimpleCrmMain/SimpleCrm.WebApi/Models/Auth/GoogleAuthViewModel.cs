using System;
namespace SimpleCrm.WebApi.Models.Auth
{
	public class GoogleAuthViewModel
	{
        public string BaseHref { get; set; }
        public string AccessToken { get; set; }
        public string State { get; set; }
    }
}

