using AutoHub.Business.Services;
using AutoHub.Data.Database;
using AutoHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.Business.Tests
{
    [TestClass]
	public class SaleServiceTests
	{
		private AutoHubDbContext _context;
		private SaleService _saleService;
		private List<Brand> _testBrands;
		private List<Car> _testCars;
		private List<Sale> _testSales;

		[TestInitialize]
		public void Setup()
		{
			// Setup in-memory database
			var options = new DbContextOptionsBuilder<AutoHubDbContext>()
				.UseInMemoryDatabase(databaseName: $"AutoHubTestDb_{Guid.NewGuid()}")
				.Options;

			_context = new AutoHubDbContext(options);

			// Initialize test data
			_testBrands = new List<Brand>
			{
				new Brand { Id = 1, Name = "Toyota", CountryOfOrigin = "Japan" },
				new Brand { Id = 2, Name = "BMW", CountryOfOrigin = "Germany" }
			};

			_testCars = new List<Car>
			{
				new Car { Id = 1, Model = "Corolla", Year = 2020, Price = 25000, BrandId = 1, Brand = _testBrands[0] },
				new Car { Id = 2, Model = "Camry", Year = 2021, Price = 30000, BrandId = 1, Brand = _testBrands[0] },
				new Car { Id = 3, Model = "X5", Year = 2022, Price = 60000, BrandId = 2, Brand = _testBrands[1] }
			};

			_testSales = new List<Sale>
			{
				new Sale { Id = 1, SaleDate = DateTime.Now.AddDays(-10), SalePrice = 24000, CarId = 1, Car = _testCars[0] },
				new Sale { Id = 2, SaleDate = DateTime.Now.AddDays(-5), SalePrice = 29000, CarId = 2, Car = _testCars[1] },
				new Sale { Id = 3, SaleDate = DateTime.Now.AddDays(-1), SalePrice = 58000, CarId = 3, Car = _testCars[2] }
			};

			_context.Brands.AddRange(_testBrands);
			_context.Cars.AddRange(_testCars);
			_context.Sales.AddRange(_testSales);
			_context.SaveChanges();

			_saleService = new SaleService(_context);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public async Task GetAllSalesAsync_ShouldReturnAllSales()
		{
			var result = await _saleService.GetAllSalesAsync();

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task GetSaleByIdAsync_WithValidId_ShouldReturnSale()
		{
			var saleId = 1;

			var result = await _saleService.GetSaleByIdAsync(saleId);

			Assert.IsNotNull(result);
			Assert.AreEqual(saleId, result.Id);
			Assert.AreEqual(24000, result.SalePrice);
		}

		[TestMethod]
		public async Task GetSaleByIdAsync_WithInvalidId_ShouldReturnNull()
		{
			var invalidSaleId = 999;

			var result = await _saleService.GetSaleByIdAsync(invalidSaleId);

			Assert.IsNull(result);
		}

		[TestMethod]
		public async Task GetSalesByCarModelAsync_WithValidCarModel_ShouldReturnMatchingSales()
		{
			var carModel = "corolla";

			var result = await _saleService.GetSalesByCarModelAsync(carModel);

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(1, result.First().CarId);
		}

		[TestMethod]
		public async Task GetSalesByCarModelAsync_WithPartialCarModel_ShouldReturnMatchingSales()
		{
			var partialCarModel = "oro";

			var result = await _saleService.GetSalesByCarModelAsync(partialCarModel);

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(1, result.First().CarId);
		}

		[TestMethod]
		public async Task GetSalesByCarModelAsync_WithEmptySearchTerm_ShouldReturnAllSales()
		{
			var emptySearchTerm = "";

			var result = await _saleService.GetSalesByCarModelAsync(emptySearchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task CreateSaleAsync_WithNullSale_ShouldThrowArgumentNullException()
		{
			Sale nullSale = null;

			await _saleService.CreateSaleAsync(nullSale);
		}

		[TestMethod]
		public async Task UpdateSaleAsync_WithValidSale_ShouldUpdateAndReturnSale()
		{
			var saleToUpdate = new Sale
			{
				Id = 1,
				SaleDate = DateTime.Now,
				SalePrice = 23000,
				CarId = 1
			};

			var result = await _saleService.UpdateSaleAsync(saleToUpdate);

			Assert.IsNotNull(result);
			Assert.AreEqual(23000, result.SalePrice);

			var updatedSaleInDb = await _context.Sales.FindAsync(1);
			Assert.IsNotNull(updatedSaleInDb);
			Assert.AreEqual(23000, updatedSaleInDb.SalePrice);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task UpdateSaleAsync_WithNullSale_ShouldThrowArgumentNullException()
		{
			Sale nullSale = null;

			await _saleService.UpdateSaleAsync(nullSale);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task UpdateSaleAsync_WithNonExistentSale_ShouldThrowKeyNotFoundException()
		{
			var nonExistentSale = new Sale
			{
				Id = 999,
				SaleDate = DateTime.Now,
				SalePrice = 25000,
				CarId = 1
			};

			await _saleService.UpdateSaleAsync(nonExistentSale);
		}

		[TestMethod]
		public async Task DeleteSaleAsync_WithValidId_ShouldReturnTrue()
		{
			var saleId = 1;

			var result = await _saleService.DeleteSaleAsync(saleId);

			Assert.IsTrue(result);

			var deletedSale = await _context.Sales.FindAsync(saleId);
			Assert.IsNull(deletedSale);
		}

		[TestMethod]
		public async Task DeleteSaleAsync_WithInvalidId_ShouldReturnFalse()
		{
			var invalidSaleId = 999;

			var result = await _saleService.DeleteSaleAsync(invalidSaleId);

			Assert.IsFalse(result);
		}
	}
}