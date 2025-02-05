using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;



        [HttpPost]
        public async Task<IActionResult> Create(UserRegistrationForm form)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.CheckIfUserExistsAsync(x => x.FirstName == form.LastName && x.LastName == form.LastName && x.Email == form.Email))
                {
                    return Conflict("User with same credentials already exists.");
                }

                var product = await _userService.CreateUserAsync(form);
                if (product != null)
                {
                    return Ok(product);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _userService.GetUserAsync(x => x.Id == id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _userService.DeleteUserAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
