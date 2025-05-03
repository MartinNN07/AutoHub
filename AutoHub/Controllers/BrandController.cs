using AutoHub.Business.Services.Interfaces;
using AutoHub.Controllers.Interfaces;
using AutoHub.Data.Models;
using AutoHub.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers
{
    public class BrandController : IBrandController
    {
        private readonly IBrandService _brandService;
        private readonly BrandView _brandView;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
            _brandView = new BrandView(brandService);
        }

        public async Task Run()
        {
            await _brandView.DisplayMenu();
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _brandService.GetAllBrandsAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _brandService.GetBrandByIdAsync(id);
        }

        public async Task<IEnumerable<Brand>> SearchBrandsByNameAsync(string searchTerm)
        {
            return await _brandService.GetBrandsByNameAsync(searchTerm);
        }

        public async Task<Brand> CreateBrandAsync(Brand brand)
        {

			// Validate incoming brand
			if (string.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ArgumentException("Brand name is required.");
            }

            if (brand.Name.Length > 50)
            {
                throw new ArgumentException("Brand name cannot exceed 50 characters.");
            }

            if (brand.CountryOfOrigin != null && brand.CountryOfOrigin.Length > 50)
            {
                throw new ArgumentException("Country of origin cannot exceed 50 characters.");
            }

            return await _brandService.CreateBrandAsync(brand);
        }

        public async Task<Brand> UpdateBrandAsync(Brand brand)
        {
            var existingBrand = await _brandService.GetBrandByIdAsync(brand.Id);

			// Validate incoming brand
			if (existingBrand == null)
            {
                throw new KeyNotFoundException($"Brand with ID {brand.Id} not found.");
            }

            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ArgumentException("Brand name is required.");
            }

            if (brand.Name.Length > 50)
            {
                throw new ArgumentException("Brand name cannot exceed 50 characters.");
            }

            if (brand.CountryOfOrigin != null && brand.CountryOfOrigin.Length > 50)
            {
                throw new ArgumentException("Country of origin cannot exceed 50 characters.");
            }

            return await _brandService.UpdateBrandAsync(brand);
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var existingBrand = await _brandService.GetBrandByIdAsync(id);
            if (existingBrand == null)
            {
                return false;
            }

			// Check if the brand has associated cars
			if (existingBrand.Cars != null && existingBrand.Cars.Any())
            {
                throw new InvalidOperationException(
                    $"Cannot delete brand with ID {id} because it has {existingBrand.Cars.Count} associated cars. " +
                    "Please reassign or delete these cars first.");
            }

            return await _brandService.DeleteBrandAsync(id);
        }
    }
}
