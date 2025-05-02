using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers.Interfaces
{
	public interface ICarController
	{
		/// <summary>
		/// Runs the car management system.
		/// </summary>
		Task Run();

		/// <summary>
		/// Retrieves all cars from the database.
		/// </summary>
		/// <returns>A collection of all cars</returns>
		Task<IEnumerable<Car>> GetAllCarsAsync();

		/// <summary>
		/// Retrieves a specific car by its ID.
		/// </summary>
		/// <param name="id">The ID of the car to retrieve</param>
		/// <returns>The car if found, null otherwise</returns>
		Task<Car> GetCarByIdAsync(int id);

		/// <summary>
		/// Searches for cars by model name.
		/// </summary>
		/// <param name="searchTerm">The search term to look for in model names</param>
		/// <returns>A collection of matching cars</returns>
		Task<IEnumerable<Car>> SearchCarsByModelAsync(string searchTerm);

		/// <summary>
		/// Creates a new car in the database.
		/// </summary>
		/// <param name="car">The car to create</param>
		/// <returns>The created car with its ID assigned</returns>
		Task<Car> CreateCarAsync(Car car);

		/// <summary>
		/// Updates an existing car in the database.
		/// </summary>
		/// <param name="car">The car with updated information</param>
		/// <returns>The updated car</returns>
		Task<Car> UpdateCarAsync(Car car);

		/// <summary>
		/// Deletes a car from the database.
		/// </summary>
		/// <param name="id">The ID of the car to delete</param>
		/// <returns>True if the car was deleted, false otherwise</returns>
		Task<bool> DeleteCarAsync(int id);

		/// <summary>
		/// Retrieves all brands from the database.
		/// </summary>
		/// <returns>A collection of all brands</returns>
		Task<IEnumerable<Brand>> GetAllBrandsAsync();
	}
}
