using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers.Interfaces
{
    public interface IBrandController
    {
        /// <summary>
        /// Runs the brand management system.
        /// </summary>
        Task Run();

        /// <summary>
        /// Retrieves all brands from the database.
        /// </summary>
        /// <returns>A collection of all brands</returns>
        Task<IEnumerable<Brand>> GetAllBrandsAsync();

        /// <summary>
        /// Retrieves a specific brand by its ID.
        /// </summary>
        /// <param name="id">The ID of the brand to retrieve</param>
        /// <returns>The brand if found, null otherwise</returns>
        Task<Brand> GetBrandByIdAsync(int id);

        /// <summary>
        /// Searches for brands by name.
        /// </summary>
        /// <param name="searchTerm">The search term to look for in brand names</param>
        /// <returns>A collection of matching brands</returns>
        Task<IEnumerable<Brand>> SearchBrandsByNameAsync(string searchTerm);

        /// <summary>
        /// Creates a new brand in the database.
        /// </summary>
        /// <param name="brand">The brand to create</param>
        /// <returns>The created brand with its ID assigned</returns>
        Task<Brand> CreateBrandAsync(Brand brand);

        /// <summary>
        /// Updates an existing brand in the database.
        /// </summary>
        /// <param name="brand">The brand with updated information</param>
        /// <returns>The updated brand</returns>
        Task<Brand> UpdateBrandAsync(Brand brand);

        /// <summary>
        /// Deletes a brand from the database.
        /// </summary>
        /// <param name="id">The ID of the brand to delete</param>
        /// <returns>True if the brand was deleted, false otherwise</returns>
        Task<bool> DeleteBrandAsync(int id);
    }
}
