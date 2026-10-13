using AutoMapper;
using InventronAPI.Data;
using InventronAPI.Models.DTOs;
using InventronAPI.Models.Entities;
using InventronAPI.Services.Interfaces;
using InventronAPI.Services.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
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

		public async Task<List<ProductDto>> GetProductsAsync()
		{
			var allProducts = await _ctx.Products.ToListAsync();
			return _mapper.Map<List<ProductDto>>(allProducts);
		}

		public async Task<ProductDto> GetByIdAsync(int id)
		{
			var productEntity = await _ctx.Products.FirstOrDefaultAsync(p => p.Id == id);
			return _mapper.Map<ProductDto>(productEntity);
		}

		public async Task<ProductDto> AddProductAsync(NewProductRequestDto product)
		{
			var newProduct = _mapper.Map<Product>(product);

			_ctx.Add(newProduct);
			await _ctx.SaveChangesAsync();

			return _mapper.Map<ProductDto>(newProduct);
		}

		public async Task<bool> AddProductsMany(List<NewProductRequestDto> products)
		{
			var newProducts = _mapper.Map<List<Product>>(products);

			_ctx.AddRange(newProducts);
			await _ctx.SaveChangesAsync();

			return true;
		}

		public async Task<bool> UpdateProductAsync(int id, ProductDto product)
		{
			var productToUpdate = await _ctx.Products.FindAsync(id);

			if (productToUpdate == null) 
			{
				return false;
			}

			_mapper.Map(product, productToUpdate);
			
			await _ctx.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteProductAsync(int id)
		{
			var productToDelete = await _ctx.Products.FindAsync(id);

			if (productToDelete == null)
			{
				return false;
			}

			_ctx.Products.Remove(productToDelete);

			await _ctx.SaveChangesAsync();

			return true;
		}
	}
}
