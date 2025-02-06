using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<Project> CreateProjectAsync(ProjectRegistrationForm form);
    Task<bool> DeleteProjectAsync(int id);
    Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync();
    Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<bool> CheckIfProjectExistsAsync(Expression<Func<ProjectEntity, bool>> expression);
}