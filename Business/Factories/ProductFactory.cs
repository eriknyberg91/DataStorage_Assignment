using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ProductFactory
{
    // CREATE PRODUCT
    public static ProductRegistrationForm Create() => new();

    public static ProductEntity Create(ProductRegistrationForm form) => new()
    {
        ProductName = form.ProductName,
        Price = form.Price
    };

    public static Product Create(ProductEntity entity) => new()
    {
        Id = entity.Id,
        ProductName = entity.ProductName,
        Price = entity.Price

    };
}
