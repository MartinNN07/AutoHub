using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers.Interfaces
{
    public interface ISaleController
    {
		/// <summary>
		/// Runs the sale management system.
		/// </summary>
		Task Run();

		/// <summary>
		/// Retrieves all sales from the database.
		/// </summary>
		/// <returns>A collection of all sales</returns>
		Task<IEnumerable<Sale>> GetAllSalesAsync();

		/// <summary>
		/// Retrieves a specific sale by its ID.
		/// </summary>
		/// <param name="id">The ID of the sale to retrieve</param>
		/// <returns>The sale if found, null otherwise</returns>
		Task<Sale> GetSaleByIdAsync(int id);

		/// <summary>
		/// Searches for sales by car model.
		/// </summary>
		/// <param name="searchTerm">The search term to look for in car models</param>
		/// <returns>A collection of matching sales</returns>
		Task<IEnumerable<Sale>> SearchSalesByCarModelAsync(string searchTerm);

		/// <summary>
		/// Creates a new sale in the database.
		/// </summary>
		/// <param name="sale">The sale to create</param>
		/// <returns>The created sale with its ID assigned</returns>
		Task<Sale> CreateSaleAsync(Sale sale);

		/// <summary>
		/// Updates an existing sale in the database.
		/// </summary>
		/// <param name="sale">The sale with updated information</param>
		/// <returns>The updated sale</returns>
		Task<Sale> UpdateSaleAsync(Sale sale);

		/// <summary>
		/// Deletes a sale from the database.
		/// </summary>
		/// <param name="id">The ID of the sale to delete</param>
		/// <returns>True if the sale was deleted, false otherwise</returns>
		Task<bool> DeleteSaleAsync(int id);

		/// <summary>
		/// Retrieves all available cars from the database.
		/// </summary>
		/// <returns>A collection of available cars</returns>
		Task<IEnumerable<Car>> GetAvailableCarsAsync();

		/// <summary>
		/// Retrieves all customers from the database.
		/// </summary>
		/// <returns>A collection of all customers</returns>
		Task<IEnumerable<Customer>> GetAllCustomersAsync();

		/// <summary>
		/// Retrieves all salespersons from the database.
		/// </summary>
		/// <returns>A collection of all salespersons</returns>
		Task<IEnumerable<Salesperson>> GetAllSalespersonsAsync();
	}
}
