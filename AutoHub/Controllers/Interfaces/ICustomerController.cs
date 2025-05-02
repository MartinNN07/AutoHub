using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers.Interfaces
{
    public interface ICustomerController
    {
        /// <summary>
        /// Runs the customer management system.
        /// </summary>
        Task Run();

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns>A collection of all customers</returns>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        /// <summary>
        /// Retrieves a specific customer by its ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve</param>
        /// <returns>The customer if found, null otherwise</returns>
        Task<Customer> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Searches for customers by first name.
        /// </summary>
        /// <param name="searchTerm">The search term to look for in first names</param>
        /// <returns>A collection of matching customers</returns>
        Task<IEnumerable<Customer>> SearchCustomersByFirstNameAsync(string searchTerm);

        /// <summary>
        /// Creates a new customer in the database.
        /// </summary>
        /// <param name="customer">The customer to create</param>
        /// <returns>The created customer with its ID assigned</returns>
        Task<Customer> CreateCustomerAsync(Customer customer);

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        /// <param name="customer">The customer with updated information</param>
        /// <returns>The updated customer</returns>
        Task<Customer> UpdateCustomerAsync(Customer customer);

        /// <summary>
        /// Deletes a customer from the database.
        /// </summary>
        /// <param name="id">The ID of the customer to delete</param>
        /// <returns>True if the customer was deleted, false otherwise</returns>
        Task<bool> DeleteCustomerAsync(int id);
    }
}
