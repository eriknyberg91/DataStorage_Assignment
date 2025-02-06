using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

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
}

