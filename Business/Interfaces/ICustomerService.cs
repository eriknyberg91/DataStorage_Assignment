using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<Customer> CreateCustomerAsync(CustomerRegistrationForm form);
    Task<bool> DeleteCustomerAsync(int id);
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
    Task<bool> CheckIfCustomerExistsAsync(Expression<Func<CustomerEntity, bool>> expression);
}
