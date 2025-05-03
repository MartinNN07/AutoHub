using AutoHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Database
{
	public class AutoHubDbContext :DbContext
	{
		public DbSet<Car> Cars { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Salesperson> Salespersons { get; set; }
		public DbSet<Sale> Sales { get; set; }

		public AutoHubDbContext(DbContextOptions<AutoHubDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//setup the relationships between the entities
			modelBuilder.Entity<Car>()
				.HasOne(c => c.Brand)
				.WithMany(b => b.Cars)
				.HasForeignKey(c => c.BrandId)
				.OnDelete(DeleteBehavior.Restrict); 

			modelBuilder.Entity<Sale>()
				.HasOne(s => s.Car)
				.WithOne()
				.HasForeignKey<Sale>(s => s.CarId)
				.OnDelete(DeleteBehavior.Restrict); 

			modelBuilder.Entity<Sale>()
				.HasOne(s => s.Customer)
				.WithMany(c => c.Sales)
				.HasForeignKey(s => s.CustomerId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Sale>()
				.HasOne(s => s.Salesperson)
				.WithMany(sp => sp.Sales)
				.HasForeignKey(s => s.SalespersonId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Salesperson>()
				.HasIndex(s => s.EmployeeNumber)
				.IsUnique();

			modelBuilder.Entity<Sale>()
				.HasIndex(s => s.CarId)
				.IsUnique();
		}
	}
}
