using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Models.Auth;

namespace SimpleCrm.WebApi.ApiControllers
{ 
    public class AuthController : Controller
		{
		private readonly UserManager<CrmUser> _userManager;


		public AuthController(UserManager<CrmUser> userManager)
		{
			_userManager = userManager;
		}

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

		private async Task<CrmUser> Authenticate(string emailAddress, string password)
		{
			if (string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(password))
				return await Task.FromResult<CrmUser>(null);

			var userToVerify = await _userManager.FindByNameAsync(emailAddress);

			if (userToVerify == null) return await Task.FromResult<CrmUser>(null);

			if (await _userManager.CheckPasswordAsync(userToVerify, password))
			{
				return await Task.FromResult(userToVerify);
			}
			return await Task.FromResult<CrmUser>(null);
		}
	}
}

