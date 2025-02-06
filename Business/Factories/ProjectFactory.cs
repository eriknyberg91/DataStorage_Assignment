using Business.Dtos;
using Business.Models;
using Data.Entities;
using Data.Migrations;

namespace Business.Factories;

public class ProjectFactory
{
    // CREATE PROJECT
    public static ProjectRegistrationForm Create() => new();
    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
    {
        Title = form.Title,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        CustomerId = form.CustomerId,
        Customer = form.Customer,
        StatusId = form.StatusId,
        Status = form.Status,
        UserId = form.UserId,
        User = form.User,
        Product = form.Product,
        ProductId = form.ProductId,
        ProjectNumber = form.ProjectNumber

    };
    public static Project Create(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        CustomerId = entity.CustomerId,
        Customer = entity.Customer,
        StatusId = entity.StatusId,
        Status = entity.Status,
        UserId = entity.UserId,
        User = entity.User,
        Product = entity.Product,
        ProductId = entity.ProductId,
        ProjectNumber = entity.ProjectNumber,
        
    };
}

