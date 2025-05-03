using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.Data.Models;

namespace AutoHub.Business.Services.Interfaces
{
	/// <summary>
	/// Provides methods to manage salesperson entities in the database.
	/// </summary>
	public interface ISalespersonService
	{
		/// <summary>
		/// Creates a new salesperson in the database.
		/// </summary>
		/// <param name="salesperson">The salesperson to create.</param>
		/// <returns>The created salesperson with their assigned ID.</returns>
		Task<Salesperson> CreateSalespersonAsync(Salesperson salesperson);

		/// <summary>
		/// Retrieves all salespersons from the database.
		/// </summary>
		/// <returns>A collection of all salespersons.</returns>
		Task<IEnumerable<Salesperson>> GetAllSalespersonAsync();

		/// <summary>
		/// Retrieves a salesperson by their unique ID.
		/// </summary>
		/// <param name="id">The ID of the salesperson to retrieve.</param>
		/// <returns>The salesperson with the specified ID, or null if not found.</returns>
		Task<Salesperson> GetSalespersonByIdAsync(int id);

		/// <summary>
		/// Searches for salespersons by their first name or partial first name.
		/// </summary>
		/// <param name="searchTerm">The term to search for in salesperson first names.</param>
		/// <returns>A collection of salespersons matching the search term.</returns>
		Task<IEnumerable<Salesperson>> GetSalespersonByFirstNameAsync(string searchTerm);

		/// <summary>
		/// Updates an existing salesperson in the database.
		/// </summary>
		/// <param name="salesperson">The salesperson with updated information.</param>
		/// <returns>The updated salesperson.</returns>
		Task<Salesperson> UpdateSalespersonAsync(Salesperson salesperson);

		/// <summary>
		/// Deletes a salesperson by their unique ID.
		/// </summary>
		/// <param name="id">The ID of the salesperson to delete.</param>
		/// <returns>True if the salesperson was successfully deleted, otherwise false.</returns>
		Task<bool> DeleteSalespersonAsync(int id);
	}
}
