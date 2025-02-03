using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class CustomerService (ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;


    public async Task<Customer> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var newCustomerEntity = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName); 
        if (newCustomerEntity == null)
        {
            newCustomerEntity = await _customerRepository.CreateAsync(CustomerFactory.Create(form));
        }

        return CustomerFactory.Create(newCustomerEntity);
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var entity = await _customerRepository.GetAsync(expression);
        var customer = CustomerFactory.Create(entity);
        return customer ?? null!;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetAsync(x => x.Id == id);
        var result = await _customerRepository.DeleteAsync(x => x.Id == id, customer);

        return result;
    }
    public async Task<bool> CheckIfCustomerExistsAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        return await _customerRepository.ExistsAsync(expression);
    }
}
