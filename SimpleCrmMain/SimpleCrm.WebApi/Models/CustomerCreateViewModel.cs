using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerCreateViewModel
    {
        [Required,MaxLength(50)]
        public string FirstName { get; set; }

        [Required,MinLength(1), MaxLength(50)]
        public string LastName { get; set; }

        [Required,MinLength(7), MaxLength(12)]
        public string PhoneNumber { get; set; }

        [Required,EmailAddress]
        public string EmailAddress { get; set; }
        
        public InteractionMethod PreferredContactMethod { get; set; }
    }
}