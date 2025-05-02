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
    public class CarServiceTests
    {
        private AutoHubDbContext _context;
        private CarService _carService;
        private List<Brand> _testBrands;
        private List<Car> _testCars;

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

            // Add test data to in-memory database
            _context.Brands.AddRange(_testBrands);
            _context.Cars.AddRange(_testCars);
            _context.SaveChanges();

            // Create the service to test with the in-memory database context
            _carService = new CarService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllCarsAsync_ShouldReturnAllCars()
        {
            // Act
            var result = await _carService.GetAllCarsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            CollectionAssert.Contains(result.Select(c => c.Model).ToList(), "Corolla");
            CollectionAssert.Contains(result.Select(c => c.Model).ToList(), "Camry");
            CollectionAssert.Contains(result.Select(c => c.Model).ToList(), "X5");
        }

        [TestMethod]
        public async Task GetCarByIdAsync_WithValidId_ShouldReturnCar()
        {
            // Arrange
            var carId = 1;

            // Act
            var result = await _carService.GetCarByIdAsync(carId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(carId, result.Id);
            Assert.AreEqual("Corolla", result.Model);
            Assert.IsNotNull(result.Brand);
            Assert.AreEqual("Toyota", result.Brand.Name);
        }

        [TestMethod]
        public async Task GetCarByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidCarId = 999;

            // Act
            var result = await _carService.GetCarByIdAsync(invalidCarId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetCarsByModelAsync_WithValidSearchTerm_ShouldReturnMatchingCars()
        {
            // Arrange
            var searchTerm = "corolla"; // Should match Corolla

            // Act
            var result = await _carService.GetCarsByModelAsync(searchTerm);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Corolla", result.First().Model);
        }

        [TestMethod]
        public async Task GetCarsByModelAsync_WithPartialSearchTerm_ShouldReturnMatchingCars()
        {
            // Arrange
            var searchTerm = "or"; // Should match Corolla

            // Act
            var result = await _carService.GetCarsByModelAsync(searchTerm);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Corolla", result.First().Model);
        }

        [TestMethod]
        public async Task GetCarsByModelAsync_WithEmptySearchTerm_ShouldReturnAllCars()
        {
            // Arrange
            var searchTerm = "";

            // Act
            var result = await _carService.GetCarsByModelAsync(searchTerm);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task CreateCarAsync_WithValidCar_ShouldAddAndReturnCar()
        {
            // Arrange
            var newCar = new Car
            {
                Id = 4,
                Model = "Prius",
                Year = 2023,
                Price = 35000,
                BrandId = 1
            };

            // Act
            var result = await _carService.CreateCarAsync(newCar);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newCar.Id, result.Id);
            Assert.AreEqual(newCar.Model, result.Model);

            // Verify the car was added to the database
            var carInDb = await _context.Cars.FindAsync(newCar.Id);
            Assert.IsNotNull(carInDb);
            Assert.AreEqual(newCar.Model, carInDb.Model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateCarAsync_WithNullCar_ShouldThrowArgumentNullException()
        {
            // Arrange
            Car nullCar = null;

            // Act
            await _carService.CreateCarAsync(nullCar);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task UpdateCarAsync_WithValidCar_ShouldUpdateAndReturnCar()
        {
            // Arrange
            var carToUpdate = new Car
            {
                Id = 1,
                Model = "Corolla Updated",
                Year = 2021,
                Price = 27000,
                BrandId = 1
            };

            // Act
            var result = await _carService.UpdateCarAsync(carToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Corolla Updated", result.Model);

            // Verify the car was updated in the database
            var updatedCarInDb = await _context.Cars.FindAsync(1);
            Assert.IsNotNull(updatedCarInDb);
            Assert.AreEqual("Corolla Updated", updatedCarInDb.Model);
            Assert.AreEqual(2021, updatedCarInDb.Year);
            Assert.AreEqual(27000, updatedCarInDb.Price);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateCarAsync_WithNullCar_ShouldThrowArgumentNullException()
        {
            // Arrange
            Car nullCar = null;

            // Act
            await _carService.UpdateCarAsync(nullCar);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task UpdateCarAsync_WithNonExistentCar_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var nonExistentCar = new Car
            {
                Id = 999,
                Model = "Non-existent",
                Year = 2023,
                Price = 30000,
                BrandId = 1
            };

            // Act
            await _carService.UpdateCarAsync(nonExistentCar);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task DeleteCarAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var carId = 1;

            // Act
            var result = await _carService.DeleteCarAsync(carId);

            // Assert
            Assert.IsTrue(result);

            // Verify the car was removed from the database
            var deletedCar = await _context.Cars.FindAsync(carId);
            Assert.IsNull(deletedCar);
        }

        [TestMethod]
        public async Task DeleteCarAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var invalidCarId = 999;

            // Act
            var result = await _carService.DeleteCarAsync(invalidCarId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
