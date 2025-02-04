using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;



        [HttpPost]
        public async Task<IActionResult> Create(ProductRegistrationForm form)
        {
            if (ModelState.IsValid)
            {
                if (await _productService.CheckIfProductExistsAsync(x => x.ProductName == form.ProductName))
                {
                    return Conflict("Product with same name already exists.");
                }

                var product = await _productService.CreateProductAsync(form);
                if (product != null)
                {
                    return Ok(product);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(x => x.Id == id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _productService.DeleteProductAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
