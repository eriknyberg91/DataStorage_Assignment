using Business.Dtos;
using Business.Interfaces;
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
    }
}
