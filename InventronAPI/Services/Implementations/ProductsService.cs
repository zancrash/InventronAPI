using AutoMapper;
using InventronAPI.Data;
using InventronAPI.Models.DTOs;
using InventronAPI.Models.Entities;
using InventronAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventronAPI.Services.Implementations
{
	public class ProductsService : IProductsService
	{
		private readonly AppDbContext _ctx;
		private readonly IMapper _mapper;

		public ProductsService(AppDbContext ctx, IMapper mapper)
		{
			_ctx = ctx;
			_mapper = mapper;
		}

		public async Task<List<ProductDto>> GetProductsAsync(CancellationToken ct = default)
		{
			var allProducts = await _ctx.Products.AsNoTracking().ToListAsync(ct);
			return _mapper.Map<List<ProductDto>>(allProducts);
		}

		public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct = default)
		{
			var productEntity = await _ctx.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);
			return productEntity is null ? null : _mapper.Map<ProductDto>(productEntity);
		}

		public async Task<ProductDto> AddProductAsync(NewProductRequestDto product, CancellationToken ct = default)
		{
			var newProduct = _mapper.Map<Product>(product);

			_ctx.Add(newProduct);
			await _ctx.SaveChangesAsync(ct);

			return _mapper.Map<ProductDto>(newProduct);
		}

		public async Task<bool> AddProductsMany(List<NewProductRequestDto> products, CancellationToken ct = default)
		{
			var newProducts = _mapper.Map<List<Product>>(products);

			_ctx.AddRange(newProducts);
			await _ctx.SaveChangesAsync(ct);

			return true;
		}

		public async Task<bool> UpdateProductAsync(int id, ProductDto product, CancellationToken ct = default)
		{
			if (id != product.Id) return false;

			var productToUpdate = await _ctx.Products.FindAsync(new object[] { id }, ct);

			if (productToUpdate == null) 
			{
				return false;
			}

			_mapper.Map(product, productToUpdate);
			
			await _ctx.SaveChangesAsync(ct);

			return true;
		}

		public async Task<bool> DeleteProductAsync(int id, CancellationToken ct = default)
		{
			var productToDelete = await _ctx.Products.FindAsync(new object[] { id }, ct);

			if (productToDelete == null)
			{
				return false;
			}

			_ctx.Products.Remove(productToDelete);

			await _ctx.SaveChangesAsync(ct);

			return true;
		}
	}
}
