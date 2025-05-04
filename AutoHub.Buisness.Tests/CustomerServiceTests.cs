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
	public class CustomerServiceTests
	{
		private AutoHubDbContext _context;
		private CustomerService _customerService;
		private List<Customer> _testCustomers;

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
			_testCustomers = new List<Customer>
			{
				new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123-456-7890" },
				new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "234-567-8901" },
				new Customer { Id = 3, FirstName = "Robert", LastName = "Johnson", Email = "robert.johnson@example.com", PhoneNumber = "345-678-9012" }
			};

			_context.Customers.AddRange(_testCustomers);
			_context.SaveChanges();

			_customerService = new CustomerService(_context);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
		{
			var result = await _customerService.GetAllCustomersAsync();

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
			CollectionAssert.Contains(result.Select(c => c.FirstName).ToList(), "John");
			CollectionAssert.Contains(result.Select(c => c.FirstName).ToList(), "Jane");
			CollectionAssert.Contains(result.Select(c => c.FirstName).ToList(), "Robert");
		}

		[TestMethod]
		public async Task GetCustomerByIdAsync_WithValidId_ShouldReturnCustomer()
		{
			var customerId = 1;

			var result = await _customerService.GetCustomerByIdAsync(customerId);

			Assert.IsNotNull(result);
			Assert.AreEqual(customerId, result.Id);
			Assert.AreEqual("John", result.FirstName);
		}

		[TestMethod]
		public async Task GetCustomerByIdAsync_WithInvalidId_ShouldReturnNull()
		{
			var invalidCustomerId = 999;

			var result = await _customerService.GetCustomerByIdAsync(invalidCustomerId);

			Assert.IsNull(result);
		}

		[TestMethod]
		public async Task GetCustomerByFirstNameAsync_WithEmptySearchTerm_ShouldReturnAllCustomers()
		{
			var searchTerm = "";

			var result = await _customerService.GetCustomerByFirstNameAsync(searchTerm);

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task CreateCustomerAsync_WithValidCustomer_ShouldAddAndReturnCustomer()
		{
			var newCustomer = new Customer { Id = 4, FirstName = "Alice", LastName = "Brown", Email = "alice.brown@example.com", PhoneNumber = "456-789-0123" };

			var result = await _customerService.CreateCustomerAsync(newCustomer);

			Assert.IsNotNull(result);
			Assert.AreEqual(newCustomer.Id, result.Id);
			Assert.AreEqual(newCustomer.FirstName, result.FirstName);

			var customerInDb = await _context.Customers.FindAsync(newCustomer.Id);
			Assert.IsNotNull(customerInDb);
			Assert.AreEqual(newCustomer.FirstName, customerInDb.FirstName);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task CreateCustomerAsync_WithNullCustomer_ShouldThrowArgumentNullException()
		{
			Customer nullCustomer = null;

			await _customerService.CreateCustomerAsync(nullCustomer);
		}

		[TestMethod]
		public async Task UpdateCustomerAsync_WithValidCustomer_ShouldUpdateAndReturnCustomer()
		{
			var customerToUpdate = new Customer { Id = 1, FirstName = "John Updated", LastName = "Doe", Email = "john.updated@example.com", PhoneNumber = "123-456-7890" };

			var result = await _customerService.UpdateCustomerAsync(customerToUpdate);

			Assert.IsNotNull(result);
			Assert.AreEqual("John Updated", result.FirstName);

			var updatedCustomerInDb = await _context.Customers.FindAsync(1);
			Assert.IsNotNull(updatedCustomerInDb);
			Assert.AreEqual("John Updated", updatedCustomerInDb.FirstName);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public async Task UpdateCustomerAsync_WithNullCustomer_ShouldThrowArgumentNullException()
		{
			Customer nullCustomer = null;

			await _customerService.UpdateCustomerAsync(nullCustomer);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task UpdateCustomerAsync_WithNonExistentCustomer_ShouldThrowKeyNotFoundException()
		{
			var nonExistentCustomer = new Customer { Id = 999, FirstName = "Non", LastName = "Existent" };

			await _customerService.UpdateCustomerAsync(nonExistentCustomer);
		}

		[TestMethod]
		public async Task DeleteCustomerAsync_WithValidId_ShouldReturnTrue()
		{
			var customerId = 1;

			var result = await _customerService.DeleteCustomerAsync(customerId);

			Assert.IsTrue(result);

			var deletedCustomer = await _context.Customers.FindAsync(customerId);
			Assert.IsNull(deletedCustomer);
		}

		[TestMethod]
		public async Task DeleteCustomerAsync_WithInvalidId_ShouldReturnFalse()
		{
			var invalidCustomerId = 999;

			var result = await _customerService.DeleteCustomerAsync(invalidCustomerId);

			Assert.IsFalse(result);
		}
	}
}