using System;
namespace SimpleCrm
{
	public class Customer
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public bool OptInNewsletter { get; set; }
		public CustomerType Type { get; set; }
	}
}

