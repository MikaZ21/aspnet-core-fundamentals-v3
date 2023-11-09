using System;
using System.Collections.Generic;

namespace SimpleCrm
{
	public interface ICustomerData
	{
		Customer Get(int id);
		List<Customer> GetAll(CustomerListParameters listParameters);
        void Add(Customer customer);
		void Update(Customer customer);
		void Delete(Customer item);
		void Commit();
    }
}

