using System;
using System.ComponentModel.DataAnnotations;


namespace SimpleCrm.Web.Models
{
	public class CustomerEditViewModel
	{
		public int Id { get; set; }
        [Display(Name = "First Name:")]
		public string FirstName { get; set; }
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
		[DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Option for news letter:")]
        public bool OptInNewsletter { get; set; }
        [Display(Name = "Type of customer:")]
        public CustomerType Type { get; set; }
	}
}

