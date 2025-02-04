using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTypeController(IStatusTypeService statusTypeService) : ControllerBase
    {
        private readonly IStatusTypeService _statusTypeService = statusTypeService;



        [HttpPost]
        public async Task<IActionResult> Create(StatusTypeRegistrationForm form)
        {
            if (ModelState.IsValid)
            {
                if (await _statusTypeService.CheckIfStatusTypeExistsAsync(x => x.StatusName == form.StatusName))
                {
                    return Conflict("Customer with same name already exists.");
                }

                var product = await _statusTypeService.CreateStatusTypeAsync(form);
                if (product != null)
                {
                    return Ok(product);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IEnumerable<StatusTypeEntity>> GetAll()
        {
            return await _statusTypeService.GetAllStatusTypesAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var status = await _statusTypeService.GetStatusTypeAsync(x => x.Id == id);
            if (status != null)
            {
                return Ok(status);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (await _statusTypeService.DeleteStatusTypeAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}