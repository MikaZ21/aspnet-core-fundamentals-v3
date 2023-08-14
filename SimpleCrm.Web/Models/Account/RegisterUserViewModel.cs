using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.Web.Models.Account
{
	public class RegisterUserViewModel
	{
		[Required, DataType(DataType.EmailAddress), DisplayName("Email")]
		public string UserName { get; set; }

		[Required, MaxLength(256), DisplayName("Name")]
		public string DisplayName { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }

		[Required, DataType(DataType.Password), Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}

