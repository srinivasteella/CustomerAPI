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
        /// Add a customer 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("AddCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (customer == null || !ModelState.IsValid) return BadRequest(ModelState);
            bool isSuccess;
            isSuccess = await _customerService.AddCustomer(customer);
            return Ok(isSuccess);
        }

        /// <summary>
        /// Update a customer 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            if (customer == null || !ModelState.IsValid) return BadRequest(ModelState);
            bool isSuccess;
            isSuccess = await _customerService.UpdateCustomer(customer);
            return Ok(isSuccess);
        }

        /// <summary>
        /// Delete a customer 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCustomer([FromBody] Customer customer)
        {
            if (customer == null || !ModelState.IsValid) return BadRequest(ModelState);
            bool isSuccess;
            isSuccess = await _customerService.DeleteCustomer(customer);
            return Ok(isSuccess);
        }


        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Get()
        {
            IEnumerable<Customer> customer;
            customer = await _customerService.GetCustomer();
            return Ok(customer);
        }

        /// <summary>
        /// Search customer 
        /// </summary>
        /// <param name="searchWord"></param>
        /// <returns></returns>
        [HttpPost("FindCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FindCustomer([FromBody] string searchWord)
        {
            if (searchWord == null || !ModelState.IsValid) return BadRequest(ModelState);
            Customer customer;
            customer = await _customerService.FindCustomer(searchWord);
            return Ok(customer);
        }
    }
}
