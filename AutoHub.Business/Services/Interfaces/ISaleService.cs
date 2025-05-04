using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.Data.Models;

namespace AutoHub.Business.Services.Interfaces
{
	/// <summary>
	/// Provides methods to manage sale entities in the database.
	/// </summary>
	public interface ISaleService
	{
		/// <summary>
		/// Creates a new sale in the database.
		/// </summary>
		/// <param name="sale">The sale to create.</param>
		/// <returns>The created sale with its assigned ID.</returns>
		Task<Sale> CreateSaleAsync(Sale sale);

		/// <summary>
		/// Retrieves all sales from the database.
		/// </summary>
		/// <returns>A collection of all sales.</returns>
		Task<IEnumerable<Sale>> GetAllSalesAsync();

		/// <summary>
		/// Retrieves a sale by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the sale to retrieve.</param>
		/// <returns>The sale with the specified ID, or null if not found.</returns>
		Task<Sale> GetSaleByIdAsync(int id);

		/// <summary>
		/// Searches for sales by the car model associated with the sale.
		/// </summary>
		/// <param name="searchTerm">The term to search for in car models.</param>
		/// <returns>A collection of sales matching the search term.</returns>
		Task<IEnumerable<Sale>> GetSalesByCarModelAsync(string searchTerm);

		/// <summary>
		/// Updates an existing sale in the database.
		/// </summary>
		/// <param name="sale">The sale with updated information.</param>
		/// <returns>The updated sale.</returns>
		Task<Sale> UpdateSaleAsync(Sale sale);

		/// <summary>
		/// Deletes a sale by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the sale to delete.</param>
		/// <returns>True if the sale was successfully deleted, otherwise false.</returns>
		Task<bool> DeleteSaleAsync(int id);
	}
}
