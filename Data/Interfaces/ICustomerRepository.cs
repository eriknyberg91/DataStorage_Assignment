using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerEntity> CreateAsync(CustomerEntity entity);
    Task<IEnumerable<CustomerEntity>> GetAllAsync();
    Task<CustomerEntity> GetAsync(int id);
    Task<CustomerEntity> UpdateAsync(CustomerEntity updatedEntity);
    Task<bool> DeleteAsync(int id);
}
