using System;
using System.ComponentModel.DataAnnotations;


namespace SimpleCrm.Web.Models
{
	public class CustomerEditViewModel
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "The First Name field is required")]
        [Display(Name = "First Name:")]
        [MinLength(3), MaxLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required")]
        [Display(Name = "Last Name:")]
        [MinLength(3), MaxLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Phone Number field is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Option for news letter:")]
        public bool OptInNewsletter { get; set; }

        [Display(Name = "Type of customer:")]
        public CustomerType Type { get; set; }
	}
}

