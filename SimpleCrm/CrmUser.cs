using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SimpleCrm
{
	public class CrmUser : IdentityUser
	{
		[MaxLength(256)]
		public string DisplayName { get; set; }
	}
}

