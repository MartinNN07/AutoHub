using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Views.Interfaces
{
    public interface ISaleView
    {
        /// <summary>
        /// Displays the main menu for sale management and handles user input.
        /// </summary>
        Task DisplayMenu();

        /// <summary>
        /// Displays detailed information about a specific sale.
        /// </summary>
        /// <param name="sale">The sale to display</param>
        Task DisplaySaleDetails(Sale sale);

        /// <summary>
        /// Displays a list of all sales in the system.
        /// </summary>
        Task DisplayAllSales();

        /// <summary>
        /// Prompts the user to enter an ID and displays the corresponding sale.
        /// </summary>
        Task FindSaleById();

        /// <summary>
        /// Prompts the user to enter a search term and displays matching sales by car model.
        /// </summary>
        Task SearchSalesByCarModel();

        /// <summary>
        /// Guides the user through adding a new sale.
        /// </summary>
        Task AddNewSale();

        /// <summary>
        /// Guides the user through updating an existing sale.
        /// </summary>
        Task UpdateSale();

        /// <summary>
        /// Guides the user through deleting a sale.
        /// </summary>
        Task DeleteSale();
    }
}
