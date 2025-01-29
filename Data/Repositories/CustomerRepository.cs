using System.Diagnostics;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : ICustomerRepository
{
    private readonly DataContext _context = context;

    //CREATE
    public async Task<CustomerEntity> CreateAsync (CustomerEntity newCustomer)
    {
        if (newCustomer == null)
        {
            return null!;
        }

        try
        {
            await _context.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }

        catch (Exception exception) 
        {
            Debug.WriteLine($"Encountered error creating new customer: {exception.Message}");
            return null!;
        }
    }
    
    //READ
    public async Task <IEnumerable<CustomerEntity>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<CustomerEntity> GetAsync (int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        if (customer == null)
        {
            return null!;
        }

        return customer;
    }

    //UPDATE
    public async Task<CustomerEntity> UpdateAsync (CustomerEntity updatedEntity)
    {
        if (updatedEntity == null)
        {
            return null!;
        }

        try
        {
            var existingEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == updatedEntity.Id);
            if (existingEntity == null)
            {
                return null!;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return updatedEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating product entity: {ex.Message}");
            return null!;
        }
    }

    //DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
