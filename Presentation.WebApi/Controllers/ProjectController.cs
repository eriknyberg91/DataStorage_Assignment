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

                var project = await _projectService.CreateProjectAsync(form);
                if (project != null)
                {
                    return Ok(project);
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
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectService.GetProjectAsync(x => x.Id == id);
            if (project != null)
            {
                return Ok(project);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (await _projectService.DeleteProjectAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                var project = await _projectService.GetProjectAsync(x => x.Id == id);
                if (project != null)
                {
                    var updatedProject = await _projectService.UpdateProjectAsync(id, form);
                    if (updatedProject != false)
                    {
                        return Ok(updatedProject);
                    }
                }
            }
            return NotFound();
        }
    }
}

