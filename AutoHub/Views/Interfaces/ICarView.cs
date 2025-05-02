using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Views.Interfaces
{
	public interface ICarView
	{
		/// <summary>
		/// Displays the main menu for car management and handles user input.
		/// </summary>
		Task DisplayMenu();

		/// <summary>
		/// Displays detailed information about a specific car.
		/// </summary>
		/// <param name="car">The car to display</param>
		Task DisplayCarDetails(Car car);

		/// <summary>
		/// Displays a list of all cars in the system.
		/// </summary>
		Task DisplayAllCars();

		/// <summary>
		/// Prompts the user to enter an ID and displays the corresponding car.
		/// </summary>
		Task FindCarById();

		/// <summary>
		/// Prompts the user to enter a search term and displays matching cars.
		/// </summary>
		Task SearchCarsByModel();

		/// <summary>
		/// Guides the user through adding a new car.
		/// </summary>
		Task AddNewCar();

		/// <summary>
		/// Guides the user through updating an existing car.
		/// </summary>
		Task UpdateCar();

		/// <summary>
		/// Guides the user through deleting a car.
		/// </summary>
		Task DeleteCar();
	}
}
