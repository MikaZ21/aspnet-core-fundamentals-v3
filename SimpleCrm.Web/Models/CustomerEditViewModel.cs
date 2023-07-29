using System;
using System.ComponentModel.DataAnnotations;


namespace SimpleCrm.Web.Models
{
	public class CustomerEditViewModel
	{
		public int Id { get; set; }

        [Required()]
        [Display(Name = "First Name:")]
        [MinLength(1), MaxLength(30)]
        public string FirstName { get; set; }

        [Required()]
        [Display(Name = "Last Name:")]
        [MinLength(1), MaxLength(30)]
        public string LastName { get; set; }

        [Required()]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Option for news letter:")]
        public bool OptInNewsletter { get; set; }

        [Display(Name = "Type of customer:")]
        public CustomerType Type { get; set; }
	}
}

