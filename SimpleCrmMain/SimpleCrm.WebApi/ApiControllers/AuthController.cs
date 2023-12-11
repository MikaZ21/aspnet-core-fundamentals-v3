using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Models.Auth;

namespace SimpleCrm.WebApi.ApiControllers
{
	public class AuthController : Controller
	{
		[HttpPost("login")]
		public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
		{
			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(ModelState);
			}

			var user = await Authenticate(credentials.EmailAddress, credentials.Password);
			if (user == null)
			{
				return UnprocessableEntity("Invalid username or password.");
			}

			var userModel = await GetUserData(user);
			return Ok(userModel);
		}
	}
}

