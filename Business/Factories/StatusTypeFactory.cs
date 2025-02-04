using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class StatusTypeFactory
{
    // CREATE STATUS TYPE
    public static StatusTypeRegistrationForm Create() => new();

    public static StatusTypeEntity Create(StatusTypeRegistrationForm form) => new()
    {
        StatusName = form.StatusName
    };

    public static StatusType Create(StatusTypeEntity entity) => new()
    {
        Id = entity.Id,
        StatusName = entity.StatusName,
    };
}
