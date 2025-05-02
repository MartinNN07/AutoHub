using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Views.Interfaces
{
    public interface IBrandView
    {
        /// <summary>
        /// Displays the main menu for brand management and handles user input.
        /// </summary>
        Task DisplayMenu();

        /// <summary>
        /// Displays detailed information about a specific brand.
        /// </summary>
        /// <param name="brand">The brand to display</param>
        Task DisplayBrandDetails(Brand brand);

        /// <summary>
        /// Displays a list of all brands in the system.
        /// </summary>
        Task DisplayAllBrands();

        /// <summary>
        /// Prompts the user to enter an ID and displays the corresponding brand.
        /// </summary>
        Task FindBrandById();

        /// <summary>
        /// Prompts the user to enter a search term and displays matching brands.
        /// </summary>
        Task SearchBrandsByName();

        /// <summary>
        /// Guides the user through adding a new brand.
        /// </summary>
        Task AddNewBrand();

        /// <summary>
        /// Guides the user through updating an existing brand.
        /// </summary>
        Task UpdateBrand();

        /// <summary>
        /// Guides the user through deleting a brand.
        /// </summary>
        Task DeleteBrand();
    }
}
