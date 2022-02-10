using CustomerAPI.Model;
using CustomerAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Add customer details
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("AddCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (customer == null || !ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = _customerService.AddCustomer(customer);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "something went wrong");//catch/throw/log
            }
            return Ok();
        }

        /// <summary>
        /// Find customer details
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("FindCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> FindCustomer([FromBody] string name)
        {
            IEnumerable<Customer> customer;

            if (string.IsNullOrEmpty(name)) return BadRequest(ModelState);

            try
            {
                customer = _customerService.GetCustomer();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "something went wrong");//catch/throw/log
            }
            return Ok(customer);
        }
    }
}
