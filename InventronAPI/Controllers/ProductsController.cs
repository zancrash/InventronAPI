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
		public async Task<ActionResult<List<ProductDto>>> Get(CancellationToken ct)
		{
			var products = await _productsService.GetProductsAsync(ct);
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto?>> GetById(int id, CancellationToken ct)
		{
			var product = await _productsService.GetByIdAsync(id, ct);

			if (product is null)
				return NotFound();

			return Ok(product);
		}

		[HttpPost("Add")]
		public async Task<ActionResult<ProductDto>> AddProduct(NewProductRequestDto product, CancellationToken ct)
		{
			var addedProduct = await _productsService.AddProductAsync(product, ct);
			return CreatedAtAction(nameof(GetById), new { id = addedProduct.Id }, addedProduct);
		}

		[HttpPost("AddList")]
		public async Task<IActionResult> AddProductsMany(List<NewProductRequestDto> products, CancellationToken ct)
		{
			return Ok(await _productsService.AddProductsMany(products, ct));
		}

		[HttpPut("Update/{id}")]
		public async Task<IActionResult> UpdateProduct(int id, ProductDto product, CancellationToken ct)
		{
			var updated = await _productsService.UpdateProductAsync(id, product, ct);
			return updated ? NoContent() : NotFound("Given product was not found.");
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> DeleteProduct(int id, CancellationToken ct) 
		{
			var deleted = await _productsService.DeleteProductAsync(id, ct);
			return deleted ? NoContent() : NotFound("Given product was not found.");
		}
	}
}
