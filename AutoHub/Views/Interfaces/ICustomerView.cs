using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Views.Interfaces
{
    public interface ICustomerView
    {
        /// <summary>
        /// Displays the main menu for customer management and handles user input.
        /// </summary>
        Task DisplayMenu();

        /// <summary>
        /// Displays detailed information about a specific customer.
        /// </summary>
        /// <param name="customer">The customer to display</param>
        Task DisplayCustomerDetails(Customer customer);

        /// <summary>
        /// Displays a list of all customers in the system.
        /// </summary>
        Task DisplayAllCustomers();

        /// <summary>
        /// Prompts the user to enter an ID and displays the corresponding customer.
        /// </summary>
        Task FindCustomerById();

        /// <summary>
        /// Prompts the user to enter a search term and displays matching customers.
        /// </summary>
        Task SearchCustomersByFirstName();

        /// <summary>
        /// Guides the user through adding a new customer.
        /// </summary>
        Task AddNewCustomer();

        /// <summary>
        /// Guides the user through updating an existing customer.
        /// </summary>
        Task UpdateCustomer();

        /// <summary>
        /// Guides the user through deleting a customer.
        /// </summary>
        Task DeleteCustomer();
    }
}
