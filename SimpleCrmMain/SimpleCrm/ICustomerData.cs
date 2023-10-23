using System;
using System.Collections.Generic;

namespace SimpleCrm
{
	public interface ICustomerData
	{
		IEnumerable<Customer> GetAll();
		Customer Get(int id);
		List<Customer> GetByStatus(CustomerStatus status, int pageIndex, int take, string orderBy);
        void Add(Customer customer);
		void Update(Customer customer);
		void Delete(int customerId);
		void Commit();
    }
}

