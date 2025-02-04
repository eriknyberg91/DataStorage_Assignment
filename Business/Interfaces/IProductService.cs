using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProductService
{
    Task<Product> CreateProductAsync(ProductRegistrationForm form);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<ProductEntity>> GetAllProductsAsync();
    Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression);
    Task<bool> CheckIfProductExistsAsync(Expression<Func<ProductEntity, bool>> expression);
}
