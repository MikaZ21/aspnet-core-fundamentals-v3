using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Auth;
using SimpleCrm.WebApi.Models.Auth;

namespace SimpleCrm.WebApi.ApiControllers
{ 
    public class AuthController : Controller
		{
		private readonly UserManager<CrmUser> _userManager;
		private readonly IJwtFactory _jwtFactory;


		public AuthController(UserManager<CrmUser> userManager, IJwtFactory jwtFactory)
		{
			_userManager = userManager;
			_jwtFactory = jwtFactory;
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

		private async Task<UserSummaryViewModel> GetUserData(CrmUser user)
		{
			if (user == null)
				return null;

			var roles = await _userManager.GetRolesAsync(user);

			if (roles.Count == 0)
			{
				roles.Add("prospect");
			}

			var jwt = await _jwtFactory.GenerateEncodedToken(user.UserName,
				_jwtFactory.GenerateClaimsIdentity(user.UserName, user.Id.ToString()));

			var userModel = new UserSummaryViewModel
			{
				Id = user.Id,
				Name = user.DisplayName,
				EmailAddress = user.Email,
				JwtToken = jwt,
				Roles = roles.ToArray(),
				AccountId = 0
			};
			return userModel;
		}
	}
}

