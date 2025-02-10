using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var newProjectEntity = await _projectRepository.GetAsync(x => x.Title == form.Title);
        if (newProjectEntity == null)
        {
            newProjectEntity = await _projectRepository.CreateAsync(ProjectFactory.Create(form));
        }
        return ProjectFactory.Create(newProjectEntity);
    }
    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllAsync();
    }
    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        var user = ProjectFactory.Create(entity);
        return user ?? null!;
    }
    public async Task<bool> DeleteProjectAsync(int id)
    {
        var user = await _projectRepository.GetAsync(x => x.Id == id);
        var result = await _projectRepository.DeleteAsync(x => x.Id == id, user);
        return result;
    }
    public async Task<bool> CheckIfProjectExistsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return await _projectRepository.ExistsAsync(expression);
    }

    //Used ChatGPT to get the update method working, primarily for the validation of each value.
    public async Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
        {
            return false; // Project not found
        }

        // Update only if the values are not null
        if (!string.IsNullOrEmpty(form.Title)) project.Title = form.Title;
        if (!string.IsNullOrEmpty(form.Description)) project.Description = form.Description;
        if (form.StartDate.HasValue) project.StartDate = form.StartDate.Value;
        if (form.EndDate.HasValue) project.EndDate = form.EndDate.Value;
        if (form.CustomerId.HasValue) project.CustomerId = form.CustomerId.Value;
        if (form.StatusId.HasValue) project.StatusId = form.StatusId.Value;
        if (form.UserId.HasValue) project.UserId = form.UserId.Value;
        if (form.ProductId.HasValue) project.ProductId = form.ProductId.Value;
        if (!string.IsNullOrEmpty(form.ProjectNumber)) project.ProjectNumber = form.ProjectNumber;

        await _projectRepository.UpdateAsync(x => x.Id == id, project);

        return true; // Project updated successfully
    }
}

