using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCrm.SqlDbServices
{
	public class SqlCustomerData : ICustomerData
	{
        private readonly SimpleCrmDbContext simpleCrmDbContext;

        public SqlCustomerData(SimpleCrmDbContext simpleCrmDbContext)
        {
            this.simpleCrmDbContext = simpleCrmDbContext;
        }

        public Customer Get(int id)
        {
            return simpleCrmDbContext.Customer.FirstOrDefault((item) => item.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return simpleCrmDbContext.Customer.ToList();
        }

        public void Add(Customer customer)
        {
            simpleCrmDbContext.Customer.Add(customer);
            simpleCrmDbContext.SaveChanges();
        }

        public void Update(Customer customer)
        {
            simpleCrmDbContext.SaveChanges();
        }
    }
}
