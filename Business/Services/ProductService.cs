using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<Product> CreateProductAsync(ProductRegistrationForm form)
    {
        var newProductEntity = await _productRepository.GetAsync(x => x.ProductName == form.ProductName);
        if (newProductEntity == null)
        {
            newProductEntity = await _productRepository.CreateAsync(ProductFactory.Create(form));
        }
        return ProductFactory.Create(newProductEntity);
    }
    public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }
    public async Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        var entity = await _productRepository.GetAsync(expression);
        var product = ProductFactory.Create(entity);
        return product ?? null!;
    }
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);
        var result = await _productRepository.DeleteAsync(x => x.Id == id, product);
        return result;
    }
    public async Task<bool> CheckIfProductExistsAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        return await _productRepository.ExistsAsync(expression);
    }
}

