using InventronAPI.Models.DTOs;

namespace InventronAPI.Services.Interfaces
{
	public interface IProductsService
	{
		Task<List<ProductDto>> GetProductsAsync();

		Task<ProductDto> GetByIdAsync(int id);

		Task<ProductDto> AddProductAsync(NewProductRequestDto product);

		Task<bool> AddProductsMany(List<NewProductRequestDto> products);

		Task<bool> UpdateProductAsync(int id, ProductDto product);

		Task<bool> DeleteProductAsync(int id);
	}
}
