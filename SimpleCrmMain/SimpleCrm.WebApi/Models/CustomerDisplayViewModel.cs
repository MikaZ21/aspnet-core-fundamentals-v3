using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerDisplayViewModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public InteractionMethod PreferredContactMethod { get; set; }
        public CustomerStatus Status { get; set; }
        public DateTimeOffset LastContactDate { get; set; }

        public CustomerDisplayViewModel() { }
        public CustomerDisplayViewModel(Customer source)
        {
            if (source == null)
                return;
            CustomerId = source.Id;
            FirstName = source.FirstName;
            LastName = source.LastName;
            PhoneNumber = source.PhoneNumber;
            EmailAddress = source.EmailAddress;
            PreferredContactMethod = source.PreferredContactMethod;
            Status = source.Status;
            LastContactDate = source.LastContactDate;
        }
    }
}