using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerUpdateViewModel
    {
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(1), MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MinLength(7), MaxLength(12)]
        public string PhoneNumber { get; set; }

        [EmailAddress, MaxLength(400), Required]
        public string EmailAddress { get; set; }

        public InteractionMethod PreferredContactMethod { get; set; }
    }
}