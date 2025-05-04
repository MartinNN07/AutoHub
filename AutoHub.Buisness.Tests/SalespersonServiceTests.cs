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
			// Setup in-memory database
			var services = new ServiceCollection();
			services.AddDbContext<AutoHubDbContext>(options =>
				options.UseInMemoryDatabase(databaseName: $"AutoHubTestDb_{Guid.NewGuid()}"));

			var serviceProvider = services.BuildServiceProvider();
			_context = serviceProvider.GetRequiredService<AutoHubDbContext>();

			// Initialize test data
			_testSalespersons = new List<Salesperson>
			{
				new Salesperson { Id = 1, FirstName = "Michael", LastName = "Scott", EmployeeNumber = "EMP001", HireDate = new DateTime(2020, 1, 15) },
				new Salesperson { Id = 2, FirstName = "Jim", LastName = "Halpert", EmployeeNumber = "EMP002", HireDate = new DateTime(2020, 3, 10) },
				new Salesperson { Id = 3, FirstName = "Pam", LastName = "Beesly", EmployeeNumber = "EMP003", HireDate = new DateTime(2020, 4, 5) }
			};

			_context.Salespersons.AddRange(_testSalespersons);
			_context.SaveChanges();

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
			var result = await _salespersonService.GetAllSalespersonAsync();

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
			CollectionAssert.Contains(result.Select(s => s.FirstName).ToList(), "Michael");
			CollectionAssert.Contains(result.Select(s => s.FirstName).ToList(), "Jim");
			CollectionAssert.Contains(result.Select(s => s.FirstName).ToList(), "Pam");
		}

		[TestMethod]
		public async Task GetSalespersonByIdAsync_WithValidId_ShouldReturnSalesperson()
		{
			var salespersonId = 1;

			var result = await _salespersonService.GetSalespersonByIdAsync(salespersonId);

			Assert.IsNotNull(result);
			Assert.AreEqual(salespersonId, result.Id);
			Assert.AreEqual("Michael", result.FirstName);
			Assert.AreEqual("Scott", result.LastName);
		}

		[TestMethod]
		public async Task GetSalespersonByIdAsync_WithInvalidId_ShouldReturnNull()
		{
			var invalidSalespersonId = 999;

			var result = await _salespersonService.GetSalespersonByIdAsync(invalidSalespersonId);

			Assert.IsNull(result);
		}

		[TestMethod]
		public async Task GetSalespersonByFirstNameAsync_WithValidSearchTerm_ShouldReturnMatchingSalespersons()
		{
			var searchTerm = "mi";

			var result = await _salespersonService.GetSalespersonByFirstNameAsync(searchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("Michael", result.First().FirstName);
		}

		[TestMethod]
		public async Task GetSalespersonByFirstNameAsync_WithEmptySearchTerm_ShouldReturnAllSalespersons()
		{
			var searchTerm = "";

			var result = await _salespersonService.GetSalespersonByFirstNameAsync(searchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task CreateSalespersonAsync_WithValidSalesperson_ShouldAddAndReturnSalesperson()
		{
			var newSalesperson = new Salesperson
			{
				Id = 4,
				FirstName = "Dwight",
				LastName = "Schrute",
				EmployeeNumber = "EMP004",
				HireDate = new DateTime(2021, 2, 20)
			};

			var result = await _salespersonService.CreateSalespersonAsync(newSalesperson);

			Assert.IsNotNull(result);
			Assert.AreEqual(newSalesperson.Id, result.Id);
			Assert.AreEqual(newSalesperson.FirstName, result.FirstName);

			var salespersonInDb = await _context.Salespersons.FindAsync(newSalesperson.Id);
			Assert.IsNotNull(salespersonInDb);
			Assert.AreEqual(newSalesperson.FirstName, salespersonInDb.FirstName);
			Assert.AreEqual(new DateTime(2021, 2, 20), salespersonInDb.HireDate);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task CreateSalespersonAsync_WithNullSalesperson_ShouldThrowArgumentNullException()
		{
			Salesperson nullSalesperson = null;

			await _salespersonService.CreateSalespersonAsync(nullSalesperson);
		}

		[TestMethod]
		public async Task UpdateSalespersonAsync_WithValidSalesperson_ShouldUpdateAndReturnSalesperson()
		{
			var salespersonToUpdate = new Salesperson
			{
				Id = 1,
				FirstName = "Michael Updated",
				LastName = "Scott",
				EmployeeNumber = "EMP001-UPD",
				HireDate = new DateTime(2020, 1, 15)
			};

			var result = await _salespersonService.UpdateSalespersonAsync(salespersonToUpdate);

			Assert.IsNotNull(result);
			Assert.AreEqual("Michael Updated", result.FirstName);
			Assert.AreEqual("EMP001-UPD", result.EmployeeNumber);

			var updatedSalespersonInDb = await _context.Salespersons.FindAsync(1);
			Assert.IsNotNull(updatedSalespersonInDb);
			Assert.AreEqual("Michael Updated", updatedSalespersonInDb.FirstName);
			Assert.AreEqual("EMP001-UPD", updatedSalespersonInDb.EmployeeNumber);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task UpdateSalespersonAsync_WithNullSalesperson_ShouldThrowArgumentNullException()
		{
			Salesperson nullSalesperson = null;

			await _salespersonService.UpdateSalespersonAsync(nullSalesperson);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task UpdateSalespersonAsync_WithNonExistentSalesperson_ShouldThrowKeyNotFoundException()
		{
			var nonExistentSalesperson = new Salesperson { Id = 999, FirstName = "Non", LastName = "Existent", EmployeeNumber = "EMP999" };

			await _salespersonService.UpdateSalespersonAsync(nonExistentSalesperson);
		}

		[TestMethod]
		public async Task DeleteSalespersonAsync_WithValidId_ShouldReturnTrue()
		{
			var salespersonId = 1;

			var result = await _salespersonService.DeleteSalespersonAsync(salespersonId);

			Assert.IsTrue(result);

			var deletedSalesperson = await _context.Salespersons.FindAsync(salespersonId);
			Assert.IsNull(deletedSalesperson);
		}

		[TestMethod]
		public async Task DeleteSalespersonAsync_WithInvalidId_ShouldReturnFalse()
		{
			var invalidSalespersonId = 999;

			var result = await _salespersonService.DeleteSalespersonAsync(invalidSalespersonId);

			Assert.IsFalse(result);
		}
	}
}