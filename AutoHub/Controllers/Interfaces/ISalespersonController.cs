using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers.Interfaces
{
    public interface ISalespersonController
    {
        /// <summary>
        /// Runs the salesperson management system.
        /// </summary>
        Task Run();

        /// <summary>
        /// Retrieves all salespersons from the database.
        /// </summary>
        /// <returns>A collection of all salespersons</returns>
        Task<IEnumerable<Salesperson>> GetAllSalespersonsAsync();

        /// <summary>
        /// Retrieves a specific salesperson by their ID.
        /// </summary>
        /// <param name="id">The ID of the salesperson to retrieve</param>
        /// <returns>The salesperson if found, null otherwise</returns>
        Task<Salesperson> GetSalespersonByIdAsync(int id);

        /// <summary>
        /// Searches for salespersons by first name.
        /// </summary>
        /// <param name="searchTerm">The search term to look for in first names</param>
        /// <returns>A collection of matching salespersons</returns>
        Task<IEnumerable<Salesperson>> SearchSalespersonsByFirstNameAsync(string searchTerm);

        /// <summary>
        /// Creates a new salesperson in the database.
        /// </summary>
        /// <param name="salesperson">The salesperson to create</param>
        /// <returns>The created salesperson with its ID assigned</returns>
        Task<Salesperson> CreateSalespersonAsync(Salesperson salesperson);

        /// <summary>
        /// Updates an existing salesperson in the database.
        /// </summary>
        /// <param name="salesperson">The salesperson with updated information</param>
        /// <returns>The updated salesperson</returns>
        Task<Salesperson> UpdateSalespersonAsync(Salesperson salesperson);

        /// <summary>
        /// Deletes a salesperson from the database.
        /// </summary>
        /// <param name="id">The ID of the salesperson to delete</param>
        /// <returns>True if the salesperson was deleted, false otherwise</returns>
        Task<bool> DeleteSalespersonAsync(int id);
    }
}
