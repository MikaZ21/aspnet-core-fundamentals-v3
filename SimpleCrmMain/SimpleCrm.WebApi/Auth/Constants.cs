﻿using System;
namespace SimpleCrm.WebApi.Auth
{
	public static class Constants
	{
		public static class JwtClaimIdentifiers
		{
			public const string Rol = "rol";
			public const string Id = "id";
		}
		public static class JwtClaims
		{
			public const string ApiAccess = "api_access";
		}
	}
}

