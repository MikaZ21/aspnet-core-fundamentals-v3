﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

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
        }

        public void Update(Customer customer)
        {
            // simpleCrmDbContext.SaveChanges();
        }

        public List<Customer> GetAll(int pageIndex, int take, string orderBy)
        {
            var sortableFields = new string[] { "FIRSTNAME", "LASTNAME", "EMAILADDRESS", "PHONENUMBER", "STATUS", "LASTCONTACTDATE" };
            var fields = (orderBy ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fields)
            {
                var x = field.Trim().ToUpper();
                var parts = x.Split(' ');

                if (parts.Length > 2)
                    throw new ArgumentException("Invalid sort option " + x);
                if (parts.Length > 1 && parts[1].ToUpper() != "DESC" && parts[1].ToUpper() != "ASC")
                    throw new ArgumentException("Invalid sort direction " + x);
                if (!sortableFields.Contains(x))
                    throw new ArgumentException("Invalid sort field " + x);
            }
            if (String.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = "LastName asc";
            }

            return simpleCrmDbContext.Customer
                //.Where(x => x.Status == status)
                .OrderBy(orderBy)
                .Skip(pageIndex * take)
                .Take(take)
                .ToList();
        }

        public void Delete(Customer item)
        {
            simpleCrmDbContext.Remove(item);
        }

        public void Delete(int customerId)
        {
            var cust = Get(customerId);
            simpleCrmDbContext.Remove(cust);
        }

        public void Commit()
        {
            simpleCrmDbContext.SaveChanges();
        }
    }
}
