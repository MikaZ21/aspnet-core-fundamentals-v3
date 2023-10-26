using System;
using System.Collections.Generic;

namespace SimpleCrm
{
	public interface ICustomerData
	{
		IEnumerable<Customer> GetAll();
		Customer Get(int id);
		List<Customer> GetAll(int pageIndex, int take, string orderBy);
        void Add(Customer customer);
		void Update(Customer customer);
		void Delete(Customer item);
		void Commit();
    }
}

