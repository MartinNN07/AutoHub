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
            // Create DbContext options for in-memory database
            var options = new DbContextOptionsBuilder<AutoHubDbContext>()
                .UseInMemoryDatabase(databaseName: $"AutoHubTestDb_{Guid.NewGuid()}")
                .Options;

            // Create context with in-memory database
            _context = new AutoHubDbContext(options);

            // Initialize test brands
            _testBrands = new List<Brand>
            {
                new Brand { Id = 1, Name = "Toyota", CountryOfOrigin = "Japan" },
                new Brand { Id = 2, Name = "BMW", CountryOfOrigin = "Germany" }
            };

            // Initialize test cars
            _testCars = new List<Car>
            {
                new Car { Id = 1, Model = "Corolla", Year = 2020, Price = 25000, BrandId = 1, Brand = _testBrands[0] },
                new Car { Id = 2, Model = "Camry", Year = 2021, Price = 30000, BrandId = 1, Brand = _testBrands[0] },
                new Car { Id = 3, Model = "X5", Year = 2022, Price = 60000, BrandId = 2, Brand = _testBrands[1] }
            };

            // Initialize test sales
            _testSales = new List<Sale>
            {
                new Sale { Id = 1, SaleDate = DateTime.Now.AddDays(-10), SalePrice = 24000, CarId = 1, Car = _testCars[0] },
                new Sale { Id = 2, SaleDate = DateTime.Now.AddDays(-5), SalePrice = 29000, CarId = 2, Car = _testCars[1] },
                new Sale { Id = 3, SaleDate = DateTime.Now.AddDays(-1), SalePrice = 58000, CarId = 3, Car = _testCars[2] }
            };

            // Add test data to in-memory database
            _context.Brands.AddRange(_testBrands);
            _context.Cars.AddRange(_testCars);
            _context.Sales.AddRange(_testSales);
            _context.SaveChanges();

            // Create the service to test with the in-memory database context
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
            // Act
            var result = await _saleService.GetAllSalesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetSaleByIdAsync_WithValidId_ShouldReturnSale()
        {
            // Arrange
            var saleId = 1;

            // Act
            var result = await _saleService.GetSaleByIdAsync(saleId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(saleId, result.Id);
            Assert.AreEqual(24000, result.SalePrice);
        }

        [TestMethod]
        public async Task GetSaleByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidSaleId = 999;

            // Act
            var result = await _saleService.GetSaleByIdAsync(invalidSaleId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetSalesByCarModelAsync_WithValidCarModel_ShouldReturnMatchingSales()
        {
            // Arrange
            var carModel = "corolla"; // Should match the Corolla car

            // Act
            var result = await _saleService.GetSalesByCarModelAsync(carModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().CarId);
        }

        [TestMethod]
        public async Task GetSalesByCarModelAsync_WithPartialCarModel_ShouldReturnMatchingSales()
        {
            // Arrange
            var partialCarModel = "oro"; // Should match Corolla

            // Act
            var result = await _saleService.GetSalesByCarModelAsync(partialCarModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().CarId);
        }

        [TestMethod]
        public async Task GetSalesByCarModelAsync_WithEmptySearchTerm_ShouldReturnAllSales()
        {
            // Arrange
            var emptySearchTerm = "";

            // Act
            var result = await _saleService.GetSalesByCarModelAsync(emptySearchTerm);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateSaleAsync_WithNullSale_ShouldThrowArgumentNullException()
        {
            // Arrange
            Sale nullSale = null;

            // Act
            await _saleService.CreateSaleAsync(nullSale);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task UpdateSaleAsync_WithValidSale_ShouldUpdateAndReturnSale()
        {
            // Arrange
            var saleToUpdate = new Sale
            {
                Id = 1,
                SaleDate = DateTime.Now,
                SalePrice = 23000, // Updated price
                CarId = 1
            };

            // Act
            var result = await _saleService.UpdateSaleAsync(saleToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(23000, result.SalePrice);

            // Verify the sale was updated in the database
            var updatedSaleInDb = await _context.Sales.FindAsync(1);
            Assert.IsNotNull(updatedSaleInDb);
            Assert.AreEqual(23000, updatedSaleInDb.SalePrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateSaleAsync_WithNullSale_ShouldThrowArgumentNullException()
        {
            // Arrange
            Sale nullSale = null;

            // Act
            await _saleService.UpdateSaleAsync(nullSale);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task UpdateSaleAsync_WithNonExistentSale_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var nonExistentSale = new Sale
            {
                Id = 999,
                SaleDate = DateTime.Now,
                SalePrice = 25000,
                CarId = 1
            };

            // Act
            await _saleService.UpdateSaleAsync(nonExistentSale);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task DeleteSaleAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var saleId = 1;

            // Act
            var result = await _saleService.DeleteSaleAsync(saleId);

            // Assert
            Assert.IsTrue(result);

            // Verify the sale was removed from the database
            var deletedSale = await _context.Sales.FindAsync(saleId);
            Assert.IsNull(deletedSale);
        }

        [TestMethod]
        public async Task DeleteSaleAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var invalidSaleId = 999;

            // Act
            var result = await _saleService.DeleteSaleAsync(invalidSaleId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}