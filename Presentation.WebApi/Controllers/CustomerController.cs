using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        
        
        [HttpPost]
        public async Task<IActionResult> Create(CustomerRegistrationForm form)
        {
            if (ModelState.IsValid)
            {
                if (await _customerService.CheckIfCustomerExistsAsync(x => x.CustomerName == form.CustomerName))
                {
                    return Conflict("Customer with same name already exists.");
                }

                var product = await _customerService.CreateCustomerAsync(form);
                if (product != null)
                {
                    return Ok(product);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task <IEnumerable<CustomerEntity>> GetAll ()
        {
            return await _customerService.GetAllCustomersAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerAsync(x => x.Id == id);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (await _customerService.DeleteCustomerAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
