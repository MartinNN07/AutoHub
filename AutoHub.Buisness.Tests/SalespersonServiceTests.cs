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
	public class SalespersonServiceTests
	{
		private AutoHubDbContext _context;
		private SalespersonService _salespersonService;
		private List<Salesperson> _testSalespersons;

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
			_testSalespersons = new List<Salesperson>
			{
				new Salesperson { Id = 1, FirstName = "Michael", LastName = "Scott", EmployeeNumber = "EMP001", HireDate = new DateTime(2020, 1, 15), Sales = new List<Sale>() },
				new Salesperson { Id = 2, FirstName = "Jim", LastName = "Halpert", EmployeeNumber = "EMP002", HireDate = new DateTime(2020, 3, 10), Sales = new List<Sale>() },
				new Salesperson { Id = 3, FirstName = "Pam", LastName = "Beesly", EmployeeNumber = "EMP003", HireDate = new DateTime(2020, 4, 5), Sales = new List<Sale>() }
			};

			// Add test data to in-memory database
			_context.Salespersons.AddRange(_testSalespersons);
			_context.SaveChanges();

			// Create the service to test with the in-memory database context
			_salespersonService = new SalespersonService(_context);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public async Task GetAllSalespersonAsync_ShouldReturnAllSalespersons()
		{
			// Act
			var result = await _salespersonService.GetAllSalespersonAsync();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
			CollectionAssert.Contains(result.Select(s => s.FirstName).ToList(), "Michael");
			CollectionAssert.Contains(result.Select(s => s.FirstName).ToList(), "Jim");
			CollectionAssert.Contains(result.Select(s => s.FirstName).ToList(), "Pam");
		}

		[TestMethod]
		public async Task GetSalespersonByIdAsync_WithValidId_ShouldReturnSalesperson()
		{
			// Arrange
			var salespersonId = 1;

			// Act
			var result = await _salespersonService.GetSalespersonByIdAsync(salespersonId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(salespersonId, result.Id);
			Assert.AreEqual("Michael", result.FirstName);
			Assert.AreEqual("Scott", result.LastName);
		}

		[TestMethod]
		public async Task GetSalespersonByIdAsync_WithInvalidId_ShouldReturnNull()
		{
			// Arrange
			var invalidSalespersonId = 999;

			// Act
			var result = await _salespersonService.GetSalespersonByIdAsync(invalidSalespersonId);

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public async Task GetSalespersonByFirstNameAsync_WithValidSearchTerm_ShouldReturnMatchingSalespersons()
		{
			// Arrange
			var searchTerm = "mi";

			// Act
			var result = await _salespersonService.GetSalespersonByFirstNameAsync(searchTerm);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("Michael", result.First().FirstName);
		}

		[TestMethod]
		public async Task GetSalespersonByFirstNameAsync_WithEmptySearchTerm_ShouldReturnAllSalespersons()
		{
			// Arrange
			var searchTerm = "";

			// Act
			var result = await _salespersonService.GetSalespersonByFirstNameAsync(searchTerm);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task CreateSalespersonAsync_WithValidSalesperson_ShouldAddAndReturnSalesperson()
		{
			// Arrange
			var newSalesperson = new Salesperson
			{
				Id = 4,
				FirstName = "Dwight",
				LastName = "Schrute",
				EmployeeNumber = "EMP004",
				HireDate = new DateTime(2021, 2, 20)
			};

			// Act
			var result = await _salespersonService.CreateSalespersonAsync(newSalesperson);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(newSalesperson.Id, result.Id);
			Assert.AreEqual(newSalesperson.FirstName, result.FirstName);

			// Verify the salesperson was added to the database
			var salespersonInDb = await _context.Salespersons.FindAsync(newSalesperson.Id);
			Assert.IsNotNull(salespersonInDb);
			Assert.AreEqual(newSalesperson.FirstName, salespersonInDb.FirstName);
			Assert.AreEqual(new DateTime(2021, 2, 20), salespersonInDb.HireDate);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task CreateSalespersonAsync_WithNullSalesperson_ShouldThrowArgumentNullException()
		{
			// Arrange
			Salesperson nullSalesperson = null;

			// Act
			await _salespersonService.CreateSalespersonAsync(nullSalesperson);

			// Assert is handled by ExpectedException
		}

		[TestMethod]
		public async Task UpdateSalespersonAsync_WithValidSalesperson_ShouldUpdateAndReturnSalesperson()
		{
			// Arrange
			var salespersonToUpdate = new Salesperson
			{
				Id = 1,
				FirstName = "Michael Updated",
				LastName = "Scott",
				EmployeeNumber = "EMP001-UPD",
				HireDate = new DateTime(2020, 1, 15)
			};

			// Act
			var result = await _salespersonService.UpdateSalespersonAsync(salespersonToUpdate);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Michael Updated", result.FirstName);
			Assert.AreEqual("EMP001-UPD", result.EmployeeNumber);

			// Verify the salesperson was updated in the database
			var updatedSalespersonInDb = await _context.Salespersons.FindAsync(1);
			Assert.IsNotNull(updatedSalespersonInDb);
			Assert.AreEqual("Michael Updated", updatedSalespersonInDb.FirstName);
			Assert.AreEqual("EMP001-UPD", updatedSalespersonInDb.EmployeeNumber);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task UpdateSalespersonAsync_WithNullSalesperson_ShouldThrowArgumentNullException()
		{
			// Arrange
			Salesperson nullSalesperson = null;

			// Act
			await _salespersonService.UpdateSalespersonAsync(nullSalesperson);

			// Assert is handled by ExpectedException
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task UpdateSalespersonAsync_WithNonExistentSalesperson_ShouldThrowKeyNotFoundException()
		{
			// Arrange
			var nonExistentSalesperson = new Salesperson { Id = 999, FirstName = "Non", LastName = "Existent", EmployeeNumber = "EMP999" };

			// Act
			await _salespersonService.UpdateSalespersonAsync(nonExistentSalesperson);

			// Assert is handled by ExpectedException
		}

		[TestMethod]
		public async Task DeleteSalespersonAsync_WithValidId_ShouldReturnTrue()
		{
			// Arrange
			var salespersonId = 1;

			// Act
			var result = await _salespersonService.DeleteSalespersonAsync(salespersonId);

			// Assert
			Assert.IsTrue(result);

			// Verify the salesperson was removed from the database
			var deletedSalesperson = await _context.Salespersons.FindAsync(salespersonId);
			Assert.IsNull(deletedSalesperson);
		}

		[TestMethod]
		public async Task DeleteSalespersonAsync_WithInvalidId_ShouldReturnFalse()
		{
			// Arrange
			var invalidSalespersonId = 999;

			// Act
			var result = await _salespersonService.DeleteSalespersonAsync(invalidSalespersonId);

			// Assert
			Assert.IsFalse(result);
		}
	}
}