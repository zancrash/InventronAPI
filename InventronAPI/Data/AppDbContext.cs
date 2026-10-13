using InventronAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventronAPI.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

		public DbSet<Supplier> Suppliers { get; set; }
	}
}
