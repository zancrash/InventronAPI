using InventronAPI.Models.DTOs;
using InventronAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventronAPI.Controllers
{
	// localhost:xxxx/api/Products
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductsService _productsService;
		public ProductsController(IProductsService productsService)
		{
			_productsService = productsService;
		}

		[HttpGet]
		public async Task<ActionResult<List<ProductDto>>> Get()
		{
			var products = await _productsService.GetProductsAsync();
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto>> GetById(int id)
		{
			var product = await _productsService.GetByIdAsync(id);

			if (product is null)
				return NotFound();

			return Ok(product);
		}

		[HttpPost("Add")]
		public async Task<ActionResult<ProductDto>> AddProduct(NewProductRequestDto product)
		{
			var addedProduct = await _productsService.AddProductAsync(product);
			return CreatedAtAction(nameof(GetById), new { id = addedProduct.Id }, addedProduct);
		}

		[HttpPost("AddList")]
		public async Task<IActionResult> AddProductsMany(List<NewProductRequestDto> products)
		{
			return Ok(await _productsService.AddProductsMany(products));
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> UpdateProduct(int id, ProductDto product)
		{
			var updated = await _productsService.UpdateProductAsync(id, product);
			return updated ? NoContent() : NotFound("Given product was not found.");
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> DeleteProduct(int id) 
		{
			var deleted = await _productsService.DeleteProductAsync(id);
			return deleted ? NoContent() : NotFound("Given product was not found.");
		}
	}
}
