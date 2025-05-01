using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.Data.Database;
using AutoHub.Data.Models;

namespace AutoHub.Business.Services
{
    public class BrandService : IBrandService
    {
        private readonly AutoHubDbContext _context;

        public BrandService(AutoHubDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> CreatebrandAsync(Brand brand)
        {
            if (brand == null)
                throw new ArgumentNullException(nameof(brand));

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return brand;
        }

        public async Task<IEnumerable<Brand>> GetAllbrandsAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> GetbrandByIdAsync(int id)
        {
            return await _context.Brands
                .Include(c => c.Sales)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Brand>> GetbrandsByNameAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllbrandsAsync();

            return await _context.Brands
                .Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<Brand> UpdatebrandAsync(Brand brand)
        {
            if (brand == null)
                throw new ArgumentNullException(nameof(brand));

            var existingbrand = await _context.Brands.FindAsync(brand.Id);

            if (existingbrand == null)
                throw new KeyNotFoundException($"brand with ID {brand.Id} not found");

            _context.Entry(existingbrand).CurrentValues.SetValues(brand);

            await _context.SaveChangesAsync();

            return existingbrand;
        }

        public async Task<bool> DeletebrandAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
                return false;

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
