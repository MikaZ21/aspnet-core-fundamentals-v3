using System;
namespace SimpleCrm
{
	public interface ICustomerData
	{
		IEnumerable<Customer> GetAll();
	}
}

