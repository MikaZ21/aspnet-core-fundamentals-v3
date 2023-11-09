using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SimpleCrm.SqlDbServices
{
	public class SqlCustomerData : ICustomerData
	{
        private SimpleCrmDbContext _context;

        public SqlCustomerData(SimpleCrmDbContext context)
        {
           _context = context;
        }

        public Customer Get(int id)
        {
            return _context.Customers.FirstOrDefault((item) => item.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            // simpleCrmDbContext.SaveChanges();
        }

        public List<Customer> GetAll(CustomerListParameters listParameters)
        {
            var sortableFields = new string[] { "FIRSTNAME", "LASTNAME", "EMAILADDRESS", "PHONENUMBER", "STATUS", "LASTCONTACTDATE" };

            var orderBy = listParameters.OrderBy;
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
                orderBy = "LastName asc, firstname asc";
            }

            IQueryable<Customer> sortedResults = _context.Customers.OrderBy(orderBy); // validated above to nothing unexpected, this is OK now.
                                                                                      // calls can be chained onto sortedResults

            if (!string.IsNullOrWhiteSpace(listParameters.LastName))
            {
                sortedResults = sortedResults
                    .Where(x => x.LastName.ToLowerInvariant() == listParameters.LastName.Trim().ToLowerInvariant());
            } // The query still is not sent to the database after this line

            if (!string.IsNullOrWhiteSpace(listParameters.Term))
            {
                sortedResults = sortedResults
                    .Where(x => (x.FirstName + " " + x.LastName).Contains(listParameters.Term)
                    || x.EmailAddress.Contains(listParameters.Term));
            }

            return sortedResults
                .Skip((listParameters.Page - 1) * listParameters.Take)
                .Take(listParameters.Take)
                .ToList();

            // Once an IQueryable is converted into an IList/List, the SQL query is finalized and sent to the database.
        }

        public void Delete(Customer item)
        {
            _context.Remove(item);
        }

        public void Delete(int customerId)
        {
            var cust = Get(customerId);
            _context.Remove(cust);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
