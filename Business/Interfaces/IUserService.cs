using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(UserRegistrationForm form);
    Task<bool> DeleteUserAsync(int id);
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    Task<User> GetUserAsync(Expression<Func<UserEntity, bool>> expression);
    Task<bool> CheckIfUserExistsAsync(Expression<Func<UserEntity, bool>> expression);
}
