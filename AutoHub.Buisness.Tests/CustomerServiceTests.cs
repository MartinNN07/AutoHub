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
            _testCustomers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123-456-7890", Sales = new List<Sale>() },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "234-567-8901", Sales = new List<Sale>() },
                new Customer { Id = 3, FirstName = "Robert", LastName = "Johnson", Email = "robert.johnson@example.com", PhoneNumber = "345-678-9012", Sales = new List<Sale>() }
            };

            // Add test data to in-memory database
            _context.Customers.AddRange(_testCustomers);
            _context.SaveChanges();

            // Create the service to test with the in-memory database context
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
            // Act
            var result = await _customerService.GetAllCustomersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            CollectionAssert.Contains(result.Select(c => c.FirstName).ToList(), "John");
            CollectionAssert.Contains(result.Select(c => c.FirstName).ToList(), "Jane");
            CollectionAssert.Contains(result.Select(c => c.FirstName).ToList(), "Robert");
        }

        [TestMethod]
        public async Task GetCustomerByIdAsync_WithValidId_ShouldReturnCustomer()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customerId, result.Id);
            Assert.AreEqual("John", result.FirstName);
        }

        [TestMethod]
        public async Task GetCustomerByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidCustomerId = 999;

            // Act
            var result = await _customerService.GetCustomerByIdAsync(invalidCustomerId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetCustomerByFirstNameAsync_WithEmptySearchTerm_ShouldReturnAllCustomers()
        {
            // Arrange
            var searchTerm = "";

            // Act
            var result = await _customerService.GetCustomerByFirstNameAsync(searchTerm);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task CreateCustomerAsync_WithValidCustomer_ShouldAddAndReturnCustomer()
        {
            // Arrange
            var newCustomer = new Customer { Id = 4, FirstName = "Alice", LastName = "Brown", Email = "alice.brown@example.com", PhoneNumber = "456-789-0123" };

            // Act
            var result = await _customerService.CreateCustomerAsync(newCustomer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newCustomer.Id, result.Id);
            Assert.AreEqual(newCustomer.FirstName, result.FirstName);

            // Verify the customer was added to the database
            var customerInDb = await _context.Customers.FindAsync(newCustomer.Id);
            Assert.IsNotNull(customerInDb);
            Assert.AreEqual(newCustomer.FirstName, customerInDb.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateCustomerAsync_WithNullCustomer_ShouldThrowArgumentNullException()
        {
            // Arrange
            Customer nullCustomer = null;

            // Act
            await _customerService.CreateCustomerAsync(nullCustomer);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task UpdateCustomerAsync_WithValidCustomer_ShouldUpdateAndReturnCustomer()
        {
            // Arrange
            var customerToUpdate = new Customer { Id = 1, FirstName = "John Updated", LastName = "Doe", Email = "john.updated@example.com", PhoneNumber = "123-456-7890" };

            // Act
            var result = await _customerService.UpdateCustomerAsync(customerToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("John Updated", result.FirstName);

            // Verify the customer was updated in the database
            var updatedCustomerInDb = await _context.Customers.FindAsync(1);
            Assert.IsNotNull(updatedCustomerInDb);
            Assert.AreEqual("John Updated", updatedCustomerInDb.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateCustomerAsync_WithNullCustomer_ShouldThrowArgumentNullException()
        {
            // Arrange
            Customer nullCustomer = null;

            // Act
            await _customerService.UpdateCustomerAsync(nullCustomer);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task UpdateCustomerAsync_WithNonExistentCustomer_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var nonExistentCustomer = new Customer { Id = 999, FirstName = "Non", LastName = "Existent" };

            // Act
            await _customerService.UpdateCustomerAsync(nonExistentCustomer);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task DeleteCustomerAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = await _customerService.DeleteCustomerAsync(customerId);

            // Assert
            Assert.IsTrue(result);

            // Verify the customer was removed from the database
            var deletedCustomer = await _context.Customers.FindAsync(customerId);
            Assert.IsNull(deletedCustomer);
        }

        [TestMethod]
        public async Task DeleteCustomerAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var invalidCustomerId = 999;

            // Act
            var result = await _customerService.DeleteCustomerAsync(invalidCustomerId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}