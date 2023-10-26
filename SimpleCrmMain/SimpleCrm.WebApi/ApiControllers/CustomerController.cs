using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleCrm.WebApi.ApiControllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerData _customerData;
        public CustomerController(ICustomerData customerData)
        {
            _customerData = customerData;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var customers = _customerData.GetAll(0, 50, "");
            var models = customers.Select(customers => new CustomerDisplayViewModel(cus));

            return Ok(models);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _customerData.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerDisplayViewModel(customer);
            return Ok(model);
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] Customer model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            //if (!ModelState.IsValid)
            //{
            //    return new ValidationFailedResult(ModelState);
            //}

            _customerData.Add(model);
            _customerData.Commit();
            return Ok(model);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Customer model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var customer = _customerData.Get(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.EmailAddress = model.EmailAddress;
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.PhoneNumber = model.PhoneNumber;
            customer.PreferredContactMethod = model.PreferredContactMethod;
            customer.Status = model.Status;

            _customerData.Update(customer);
            _customerData.Commit();
            return Ok(customer);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerData.Get(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerData.Delete(customer);
            _customerData.Commit();
            return NoContent();
        }
    }
}

