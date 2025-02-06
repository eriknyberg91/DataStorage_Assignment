using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;



        [HttpPost]
        public async Task<IActionResult> Create(ProjectRegistrationForm form)
        {
            if (ModelState.IsValid)
            {
                if (await _projectService.CheckIfProjectExistsAsync(x => x.Title == form.Title))
                {
                    return Conflict("Project with same title already exists.");
                }

                var product = await _projectService.CreateProjectAsync(form);
                if (product != null)
                {
                    return Ok(product);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectEntity>> GetAll()
        {
            return await _projectService.GetAllProjectsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _projectService.GetProjectAsync(x => x.Id == id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _projectService.DeleteProjectAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}

