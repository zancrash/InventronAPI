using InventronAPI.Models.DTOs;

namespace InventronAPI.Services.Interfaces
{
	public interface IProductsService
	{
		Task<List<ProductDto>> GetProductsAsync(CancellationToken ct = default);

		Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct = default);

		Task<ProductDto> AddProductAsync(NewProductRequestDto product, CancellationToken ct = default);

		Task<bool> AddProductsMany(List<NewProductRequestDto> products, CancellationToken ct = default);

		Task<bool> UpdateProductAsync(int id, ProductDto product, CancellationToken ct = default);

		Task<bool> DeleteProductAsync(int id, CancellationToken ct = default);
	}
}
