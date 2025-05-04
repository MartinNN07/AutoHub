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

			_context.Brands.AddRange(_testBrands);
			_context.Cars.AddRange(_testCars);
			_context.SaveChanges();

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
			var result = await _carService.GetAllCarsAsync();

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
			CollectionAssert.Contains(result.Select(c => c.Model).ToList(), "Corolla");
			CollectionAssert.Contains(result.Select(c => c.Model).ToList(), "Camry");
			CollectionAssert.Contains(result.Select(c => c.Model).ToList(), "X5");
		}

		[TestMethod]
		public async Task GetCarByIdAsync_WithValidId_ShouldReturnCar()
		{
			var carId = 1;

			var result = await _carService.GetCarByIdAsync(carId);

			Assert.IsNotNull(result);
			Assert.AreEqual(carId, result.Id);
			Assert.AreEqual("Corolla", result.Model);
			Assert.IsNotNull(result.Brand);
			Assert.AreEqual("Toyota", result.Brand.Name);
		}

		[TestMethod]
		public async Task GetCarByIdAsync_WithInvalidId_ShouldReturnNull()
		{
			var invalidCarId = 999;

			var result = await _carService.GetCarByIdAsync(invalidCarId);

			Assert.IsNull(result);
		}

		[TestMethod]
		public async Task GetCarsByModelAsync_WithValidSearchTerm_ShouldReturnMatchingCars()
		{
			var searchTerm = "corolla";

			var result = await _carService.GetCarsByModelAsync(searchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("Corolla", result.First().Model);
		}

		[TestMethod]
		public async Task GetCarsByModelAsync_WithPartialSearchTerm_ShouldReturnMatchingCars()
		{
			var searchTerm = "or";

			var result = await _carService.GetCarsByModelAsync(searchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("Corolla", result.First().Model);
		}

		[TestMethod]
		public async Task GetCarsByModelAsync_WithEmptySearchTerm_ShouldReturnAllCars()
		{
			var searchTerm = "";

			var result = await _carService.GetCarsByModelAsync(searchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task CreateCarAsync_WithValidCar_ShouldAddAndReturnCar()
		{
			var newCar = new Car
			{
				Id = 4,
				Model = "Prius",
				Year = 2023,
				Price = 35000,
				BrandId = 1
			};

			var result = await _carService.CreateCarAsync(newCar);

			Assert.IsNotNull(result);
			Assert.AreEqual(newCar.Id, result.Id);
			Assert.AreEqual(newCar.Model, result.Model);

			var carInDb = await _context.Cars.FindAsync(newCar.Id);
			Assert.IsNotNull(carInDb);
			Assert.AreEqual(newCar.Model, carInDb.Model);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task CreateCarAsync_WithNullCar_ShouldThrowArgumentNullException()
		{
			Car nullCar = null;

			await _carService.CreateCarAsync(nullCar);
		}

		[TestMethod]
		public async Task UpdateCarAsync_WithValidCar_ShouldUpdateAndReturnCar()
		{
			var carToUpdate = new Car
			{
				Id = 1,
				Model = "Corolla Updated",
				Year = 2021,
				Price = 27000,
				BrandId = 1
			};

			var result = await _carService.UpdateCarAsync(carToUpdate);

			Assert.IsNotNull(result);
			Assert.AreEqual("Corolla Updated", result.Model);

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
			Car nullCar = null;

			await _carService.UpdateCarAsync(nullCar);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task UpdateCarAsync_WithNonExistentCar_ShouldThrowKeyNotFoundException()
		{
			var nonExistentCar = new Car
			{
				Id = 999,
				Model = "Non-existent",
				Year = 2023,
				Price = 30000,
				BrandId = 1
			};

			await _carService.UpdateCarAsync(nonExistentCar);
		}

		[TestMethod]
		public async Task DeleteCarAsync_WithValidId_ShouldReturnTrue()
		{
			var carId = 1;

			var result = await _carService.DeleteCarAsync(carId);

			Assert.IsTrue(result);

			var deletedCar = await _context.Cars.FindAsync(carId);
			Assert.IsNull(deletedCar);
		}

		[TestMethod]
		public async Task DeleteCarAsync_WithInvalidId_ShouldReturnFalse()
		{
			var invalidCarId = 999;

			var result = await _carService.DeleteCarAsync(invalidCarId);

			Assert.IsFalse(result);
		}
	}
}
