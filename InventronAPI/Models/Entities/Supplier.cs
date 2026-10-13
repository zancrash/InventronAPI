namespace InventronAPI.Models.Entities
{
	public class Supplier
	{
		public int Id { get; set; }

		public required string Company { get; set; }

		public string? Email { get; set; }

		public string? Phone { get; set; }
	}
}
