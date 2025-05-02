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
			// Create a new ServiceCollection
			var services = new ServiceCollection();

			// Setup in-memory database
			services.AddDbContext<AutoHubDbContext>(options =>
				options.UseInMemoryDatabase(databaseName: $"AutoHubTestDb_{Guid.NewGuid()}"));

			// Build the service provider
			var serviceProvider = services.BuildServiceProvider();

			// Get the DbContext instance
			_context = serviceProvider.GetRequiredService<AutoHubDbContext>();

			// Initialize test data
			_testBrands = new List<Brand>
			{
				new Brand { Id = 1, Name = "Toyota", CountryOfOrigin = "Japan", Cars = new List<Car>() },
				new Brand { Id = 2, Name = "BMW", CountryOfOrigin = "Germany", Cars = new List<Car>() },
				new Brand { Id = 3, Name = "Ford", CountryOfOrigin = "USA", Cars = new List<Car>() }
			};

			// Add test data to in-memory database
			_context.Brands.AddRange(_testBrands);
			_context.SaveChanges();

			// Create the service to test with the in-memory database context
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
			// Act
			var result = await _brandService.GetAllBrandsAsync();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
			CollectionAssert.Contains(result.Select(b => b.Name).ToList(), "Toyota");
			CollectionAssert.Contains(result.Select(b => b.Name).ToList(), "BMW");
			CollectionAssert.Contains(result.Select(b => b.Name).ToList(), "Ford");
		}

		[TestMethod]
		public async Task GetBrandByIdAsync_WithValidId_ShouldReturnBrand()
		{
			// Arrange
			var brandId = 1;

			// Act
			var result = await _brandService.GetBrandByIdAsync(brandId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(brandId, result.Id);
			Assert.AreEqual("Toyota", result.Name);
		}

		[TestMethod]
		public async Task GetBrandByIdAsync_WithInvalidId_ShouldReturnNull()
		{
			// Arrange
			var invalidBrandId = 999;

			// Act
			var result = await _brandService.GetBrandByIdAsync(invalidBrandId);

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public async Task GetBrandsByNameAsync_WithValidSearchTerm_ShouldReturnMatchingBrands()
		{
			// Arrange
			var searchTerm = "to";

			// Act
			var result = await _brandService.GetBrandsByNameAsync(searchTerm);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("Toyota", result.First().Name);
		}

		[TestMethod]
		public async Task GetBrandsByNameAsync_WithEmptySearchTerm_ShouldReturnAllBrands()
		{
			// Arrange
			var searchTerm = "";

			// Act
			var result = await _brandService.GetBrandsByNameAsync(searchTerm);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task CreateBrandAsync_WithValidBrand_ShouldAddAndReturnBrand()
		{
			// Arrange
			var newBrand = new Brand { Id = 4, Name = "Honda", CountryOfOrigin = "Japan" };

			// Act
			var result = await _brandService.CreateBrandAsync(newBrand);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(newBrand.Id, result.Id);
			Assert.AreEqual(newBrand.Name, result.Name);

			// Verify the brand was added to the database
			var brandInDb = await _context.Brands.FindAsync(newBrand.Id);
			Assert.IsNotNull(brandInDb);
			Assert.AreEqual(newBrand.Name, brandInDb.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task CreateBrandAsync_WithNullBrand_ShouldThrowArgumentNullException()
		{
			// Arrange
			Brand nullBrand = null;

			// Act
			await _brandService.CreateBrandAsync(nullBrand);

			// Assert is handled by ExpectedException
		}

		[TestMethod]
		public async Task UpdateBrandAsync_WithValidBrand_ShouldUpdateAndReturnBrand()
		{
			// Arrange
			var brandToUpdate = new Brand { Id = 1, Name = "Toyota Updated", CountryOfOrigin = "Japan" };

			// Act
			var result = await _brandService.UpdateBrandAsync(brandToUpdate);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Toyota Updated", result.Name);

			// Verify the brand was updated in the database
			var updatedBrandInDb = await _context.Brands.FindAsync(1);
			Assert.IsNotNull(updatedBrandInDb);
			Assert.AreEqual("Toyota Updated", updatedBrandInDb.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task UpdateBrandAsync_WithNullBrand_ShouldThrowArgumentNullException()
		{
			// Arrange
			Brand nullBrand = null;

			// Act
			await _brandService.UpdateBrandAsync(nullBrand);

			// Assert is handled by ExpectedException
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task UpdateBrandAsync_WithNonExistentBrand_ShouldThrowKeyNotFoundException()
		{
			// Arrange
			var nonExistentBrand = new Brand { Id = 999, Name = "Non-existent" };

			// Act
			await _brandService.UpdateBrandAsync(nonExistentBrand);

			// Assert is handled by ExpectedException
		}

		[TestMethod]
		public async Task DeleteBrandAsync_WithValidId_ShouldReturnTrue()
		{
			// Arrange
			var brandId = 1;

			// Act
			var result = await _brandService.DeleteBrandAsync(brandId);

			// Assert
			Assert.IsTrue(result);

			// Verify the brand was removed from the database
			var deletedBrand = await _context.Brands.FindAsync(brandId);
			Assert.IsNull(deletedBrand);
		}

		[TestMethod]
		public async Task DeleteBrandAsync_WithInvalidId_ShouldReturnFalse()
		{
			// Arrange
			var invalidBrandId = 999;

			// Act
			var result = await _brandService.DeleteBrandAsync(invalidBrandId);

			// Assert
			Assert.IsFalse(result);
		}
	}
}