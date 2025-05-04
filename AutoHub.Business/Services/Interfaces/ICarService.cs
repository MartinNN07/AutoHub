using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Business.Services.Interfaces
{
	/// <summary>
	/// Provides methods to manage car entities in the database.
	/// </summary>
	public interface ICarService
	{
		/// <summary>
		/// Creates a new car in the database.
		/// </summary>
		/// <param name="car">The car to create.</param>
		/// <returns>The created car with its assigned ID.</returns>
		Task<Car> CreateCarAsync(Car car);

		/// <summary>
		/// Retrieves all cars from the database.
		/// </summary>
		/// <returns>A collection of all cars.</returns>
		Task<IEnumerable<Car>> GetAllCarsAsync();

		/// <summary>
		/// Retrieves a car by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the car to retrieve.</param>
		/// <returns>The car with the specified ID, or null if not found.</returns>
		Task<Car> GetCarByIdAsync(int id);

		/// <summary>
		/// Searches for cars by a model name or partial name.
		/// </summary>
		/// <param name="searchTerm">The term to search for in car models.</param>
		/// <returns>A collection of cars matching the search term.</returns>
		Task<IEnumerable<Car>> GetCarsByModelAsync(string searchTerm);

		/// <summary>
		/// Updates an existing car in the database.
		/// </summary>
		/// <param name="car">The car with updated information.</param>
		/// <returns>The updated car.</returns>
		Task<Car> UpdateCarAsync(Car car);

		/// <summary>
		/// Deletes a car by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the car to delete.</param>
		/// <returns>True if the car was successfully deleted, otherwise false.</returns>
		Task<bool> DeleteCarAsync(int id);
	}
}
