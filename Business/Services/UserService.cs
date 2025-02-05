using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<User> CreateUserAsync(UserRegistrationForm form)
    {
        var newUserEntity = await _userRepository.GetAsync(x => x.FirstName == form.LastName && x.LastName == form.LastName && x.Email == form.Email);
        if (newUserEntity == null)
        {
            newUserEntity = await _userRepository.CreateAsync(UserFactory.Create(form));
        }
        return UserFactory.Create(newUserEntity);
    }
    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }
    public async Task<User> GetUserAsync(Expression<Func<UserEntity, bool>> expression)
    {
        var entity = await _userRepository.GetAsync(expression);
        var user = UserFactory.Create(entity);
        return user ?? null!;
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id);
        var result = await _userRepository.DeleteAsync(x => x.Id == id, user);
        return result;
    }
    public async Task<bool> CheckIfUserExistsAsync(Expression<Func<UserEntity, bool>> expression)
    {
        return await _userRepository.ExistsAsync(expression);
    }
}

