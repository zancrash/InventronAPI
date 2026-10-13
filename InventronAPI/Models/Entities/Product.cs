namespace InventronAPI.Models.Entities
{
	public class Product
	{
		public int Id { get; set; }

		public required string Name { get; set; }

		public required string SKU { get; set; }

		public string? Description { get; set; }

		public required double Cost { get; set; }

		public required double SellingPrice { get; set; }

		public int? Quantity { get; set; }
	}
}
