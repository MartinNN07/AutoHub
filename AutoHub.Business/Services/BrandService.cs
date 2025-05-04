using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.Data.Database;
using Microsoft.EntityFrameworkCore;
using AutoHub.Business.Services.Interfaces;

namespace AutoHub.Business.Services
{
	public class BrandService : IBrandService
	{
		private readonly AutoHubDbContext _context;

		public BrandService(AutoHubDbContext context)
		{
			_context = context;
		}
		public async Task<Brand> CreateBrandAsync(Brand brand)
		{
            // Validate the brand object
            if (brand == null)
				throw new ArgumentNullException(nameof(brand));

			await _context.Brands.AddAsync(brand);
			await _context.SaveChangesAsync();

			return brand;
		}

		public async Task<bool> DeleteBrandAsync(int id)
		{
            // Validate the ID
            var brand = await _context.Brands.FindAsync(id);

			if (brand == null)
				return false;

			_context.Brands.Remove(brand);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
		{
            // Fetch all brands from the database
            return await _context.Brands.ToListAsync();
		}

		public async Task<Brand> GetBrandByIdAsync(int id)
		{
            // Validate the ID
            return await _context.Brands
				.Include(b => b.Cars)
				.FirstOrDefaultAsync(b => b.Id == id);
		}

		public async Task<IEnumerable<Brand>> GetBrandsByNameAsync(string searchTerm)
		{
            // Validate the search term
            if (string.IsNullOrWhiteSpace(searchTerm))
				return await GetAllBrandsAsync();

			return await _context.Brands
				.Where(b => b.Name.ToLower().Contains(searchTerm.ToLower()))
				.ToListAsync();
		}

		public async Task<Brand> UpdateBrandAsync(Brand brand)
		{
            // Validate the brand object
            if (brand == null)
				throw new ArgumentNullException(nameof(brand));

			var existingBrand = await _context.Brands.FindAsync(brand.Id);

			if (existingBrand == null)
				throw new KeyNotFoundException($"Brand with ID {brand.Id} not found");

			_context.Entry(existingBrand).CurrentValues.SetValues(brand);

			await _context.SaveChangesAsync();

			return existingBrand;
		}
	}
}
