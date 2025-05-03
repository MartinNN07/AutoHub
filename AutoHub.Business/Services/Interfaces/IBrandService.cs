using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.Data.Models;

namespace AutoHub.Business.Services.Interfaces
{
    public interface IBrandService
    {
		/// <summary>
		/// Creates a new brand in the database.
		/// </summary>
		/// <param name="brand">The brand to create.</param>
		/// <returns>The created brand with its assigned ID.</returns>
		Task<Brand> CreateBrandAsync(Brand brand);

		/// <summary>
		/// Retrieves all brands from the database.
		/// </summary>
		/// <returns>A collection of all brands.</returns>
		Task<IEnumerable<Brand>> GetAllBrandsAsync();
		/// <summary>
		/// Retrieves a brand by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the brand to retrieve.</param>
		/// <returns>The brand with the specified ID, or null if not found.</returns>
		Task<Brand> GetBrandByIdAsync(int id);
		/// <summary>
		/// Searches for brands by a name or partial name.
		/// </summary>
		/// <param name="searchTerm">The term to search for in brand names.</param>
		/// <returns>A collection of brands matching the search term.</returns>
		Task<IEnumerable<Brand>> GetBrandsByNameAsync(string searchTerm);

		/// <summary>
		/// Updates an existing brand in the database.
		/// </summary>
		/// <param name="brand">The brand with updated information.</param>
		/// <returns>The updated brand.</returns>
		Task<Brand> UpdateBrandAsync(Brand brand);

		/// <summary>
		/// Deletes a brand by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the brand to delete.</param>
		/// <returns>True if the brand was successfully deleted, otherwise false.</returns>
		Task<bool> DeleteBrandAsync(int id);
    }
}