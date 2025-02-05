using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class UserFactory
{
    // CREATE USER
    public static UserRegistrationForm Create() => new();
    public static UserEntity Create(UserRegistrationForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email

    };
    public static User Create(UserEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName
    };
}

