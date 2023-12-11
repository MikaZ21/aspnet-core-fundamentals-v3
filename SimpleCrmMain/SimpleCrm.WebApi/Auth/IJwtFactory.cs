using System;
using System.Security.Claims;

namespace SimpleCrm.WebApi.Auth
{
	public interface IJwtFactory
	{
		Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
		ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
	}
}

