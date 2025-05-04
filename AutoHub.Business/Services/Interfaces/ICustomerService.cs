using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Business.Services.Interfaces
{
	/// <summary>
	/// Provides methods to manage customer entities in the database.
	/// </summary>
	public interface ICustomerService
	{
		/// <summary>
		/// Creates a new customer in the database.
		/// </summary>
		/// <param name="customer">The customer to create.</param>
		/// <returns>The created customer with its assigned ID.</returns>
		Task<Customer> CreateCustomerAsync(Customer customer);

		/// <summary>
		/// Retrieves all customers from the database.
		/// </summary>
		/// <returns>A collection of all customers.</returns>
		Task<IEnumerable<Customer>> GetAllCustomersAsync();

		/// <summary>
		/// Retrieves a customer by their unique ID.
		/// </summary>
		/// <param name="id">The ID of the customer to retrieve.</param>
		/// <returns>The customer with the specified ID, or null if not found.</returns>
		Task<Customer> GetCustomerByIdAsync(int id);

		/// <summary>
		/// Searches for customers by their first name or partial first name.
		/// </summary>
		/// <param name="searchTerm">The term to search for in customer first names.</param>
		/// <returns>A collection of customers matching the search term.</returns>
		Task<IEnumerable<Customer>> GetCustomerByFirstNameAsync(string searchTerm);

		/// <summary>
		/// Updates an existing customer in the database.
		/// </summary>
		/// <param name="customer">The customer with updated information.</param>
		/// <returns>The updated customer.</returns>
		Task<Customer> UpdateCustomerAsync(Customer customer);

		/// <summary>
		/// Deletes a customer by their unique ID.
		/// </summary>
		/// <param name="id">The ID of the customer to delete.</param>
		/// <returns>True if the customer was successfully deleted, otherwise false.</returns>
		Task<bool> DeleteCustomerAsync(int id);
	}
}
