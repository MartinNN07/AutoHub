using AutoHub.Business.Services;
using AutoHub.Data.Database;
using AutoHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.Business.Tests
{
	[TestClass]
	public class BrandServiceTests
	{
		private AutoHubDbContext _context;
		private BrandService _brandService;
		private List<Brand> _testBrands;

		[TestInitialize]
		public void Setup()
		{
			var services = new ServiceCollection();

			// Setup in-memory database
			services.AddDbContext<AutoHubDbContext>(options =>
				options.UseInMemoryDatabase(databaseName: $"AutoHubTestDb_{Guid.NewGuid()}"));

			var serviceProvider = services.BuildServiceProvider();
			_context = serviceProvider.GetRequiredService<AutoHubDbContext>();

			// Initialize test data
			_testBrands = new List<Brand>
			{
				new Brand { Id = 1, Name = "Toyota", CountryOfOrigin = "Japan" },
				new Brand { Id = 2, Name = "BMW", CountryOfOrigin = "Germany" },
				new Brand { Id = 3, Name = "Ford", CountryOfOrigin = "USA" }
			};

			_context.Brands.AddRange(_testBrands);
			_context.SaveChanges();

			_brandService = new BrandService(_context);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public async Task GetAllBrandsAsync_ShouldReturnAllBrands()
		{
			var result = await _brandService.GetAllBrandsAsync();

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
			CollectionAssert.Contains(result.Select(b => b.Name).ToList(), "Toyota");
			CollectionAssert.Contains(result.Select(b => b.Name).ToList(), "BMW");
			CollectionAssert.Contains(result.Select(b => b.Name).ToList(), "Ford");
		}

		[TestMethod]
		public async Task GetBrandByIdAsync_WithValidId_ShouldReturnBrand()
		{
			var brandId = 1;

			var result = await _brandService.GetBrandByIdAsync(brandId);

			Assert.IsNotNull(result);
			Assert.AreEqual(brandId, result.Id);
			Assert.AreEqual("Toyota", result.Name);
		}

		[TestMethod]
		public async Task GetBrandByIdAsync_WithInvalidId_ShouldReturnNull()
		{
			var invalidBrandId = 999;

			var result = await _brandService.GetBrandByIdAsync(invalidBrandId);

			Assert.IsNull(result);
		}

		[TestMethod]
		public async Task GetBrandsByNameAsync_WithValidSearchTerm_ShouldReturnMatchingBrands()
		{
			var searchTerm = "to";

			var result = await _brandService.GetBrandsByNameAsync(searchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("Toyota", result.First().Name);
		}

		[TestMethod]
		public async Task GetBrandsByNameAsync_WithEmptySearchTerm_ShouldReturnAllBrands()
		{
			var searchTerm = "";

			var result = await _brandService.GetBrandsByNameAsync(searchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task CreateBrandAsync_WithValidBrand_ShouldAddAndReturnBrand()
		{
			var newBrand = new Brand { Id = 4, Name = "Honda", CountryOfOrigin = "Japan" };

			var result = await _brandService.CreateBrandAsync(newBrand);

			Assert.IsNotNull(result);
			Assert.AreEqual(newBrand.Id, result.Id);
			Assert.AreEqual(newBrand.Name, result.Name);

			var brandInDb = await _context.Brands.FindAsync(newBrand.Id);
			Assert.IsNotNull(brandInDb);
			Assert.AreEqual(newBrand.Name, brandInDb.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task CreateBrandAsync_WithNullBrand_ShouldThrowArgumentNullException()
		{
			Brand nullBrand = null;

			await _brandService.CreateBrandAsync(nullBrand);
		}

		[TestMethod]
		public async Task UpdateBrandAsync_WithValidBrand_ShouldUpdateAndReturnBrand()
		{
			var brandToUpdate = new Brand { Id = 1, Name = "Toyota Updated", CountryOfOrigin = "Japan" };

			var result = await _brandService.UpdateBrandAsync(brandToUpdate);

			Assert.IsNotNull(result);
			Assert.AreEqual("Toyota Updated", result.Name);

			var updatedBrandInDb = await _context.Brands.FindAsync(1);
			Assert.IsNotNull(updatedBrandInDb);
			Assert.AreEqual("Toyota Updated", updatedBrandInDb.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task UpdateBrandAsync_WithNullBrand_ShouldThrowArgumentNullException()
		{
			Brand nullBrand = null;

			await _brandService.UpdateBrandAsync(nullBrand);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task UpdateBrandAsync_WithNonExistentBrand_ShouldThrowKeyNotFoundException()
		{
			var nonExistentBrand = new Brand { Id = 999, Name = "Non-existent" };

			await _brandService.UpdateBrandAsync(nonExistentBrand);
		}

		[TestMethod]
		public async Task DeleteBrandAsync_WithValidId_ShouldReturnTrue()
		{
			var brandId = 1;

			var result = await _brandService.DeleteBrandAsync(brandId);

			Assert.IsTrue(result);

			var deletedBrand = await _context.Brands.FindAsync(brandId);
			Assert.IsNull(deletedBrand);
		}

		[TestMethod]
		public async Task DeleteBrandAsync_WithInvalidId_ShouldReturnFalse()
		{
			var invalidBrandId = 999;

			var result = await _brandService.DeleteBrandAsync(invalidBrandId);

			Assert.IsFalse(result);
		}
	}
}