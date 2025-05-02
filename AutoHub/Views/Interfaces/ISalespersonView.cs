using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Views.Interfaces
{
    public interface ISalespersonView
    {
        /// <summary>
        /// Displays the main menu for salesperson management and handles user input.
        /// </summary>
        Task DisplayMenu();

        /// <summary>
        /// Displays detailed information about a specific salesperson.
        /// </summary>
        /// <param name="salesperson">The salesperson to display</param>
        Task DisplaySalespersonDetails(Salesperson salesperson);

        /// <summary>
        /// Displays a list of all salespersons in the system.
        /// </summary>
        Task DisplayAllSalespersons();

        /// <summary>
        /// Prompts the user to enter an ID and displays the corresponding salesperson.
        /// </summary>
        Task FindSalespersonById();

        /// <summary>
        /// Prompts the user to enter a search term and displays matching salespersons.
        /// </summary>
        Task SearchSalespersonsByFirstName();

        /// <summary>
        /// Guides the user through adding a new salesperson.
        /// </summary>
        Task AddNewSalesperson();

        /// <summary>
        /// Guides the user through updating an existing salesperson.
        /// </summary>
        Task UpdateSalesperson();

        /// <summary>
        /// Guides the user through deleting a salesperson.
        /// </summary>
        Task DeleteSalesperson();
    }
}
