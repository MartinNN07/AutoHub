using AutoHub.Business.Services;
using AutoHub.Business.Services.Interfaces;
using AutoHub.Data.Models;
using AutoHub.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Views
{
	public class CarView : ICarView
	{
		private readonly ICarService _carService;
		private readonly IBrandService _brandService;

		public CarView(ICarService carService, IBrandService brandService)
		{
			_carService = carService;
			_brandService = brandService;
		}

		public async Task DisplayMenu()
		{
			bool exit = false;
			while (!exit)
			{
				Console.Clear();
				Console.WriteLine("========== Car Management ==========");
				Console.WriteLine("1. View All Cars");
				Console.WriteLine("2. Find Car by ID");
				Console.WriteLine("3. Search Cars by Model");
				Console.WriteLine("4. Add New Car");
				Console.WriteLine("5. Update Car");
				Console.WriteLine("6. Delete Car");
				Console.WriteLine("0. Back to Main Menu");
				Console.WriteLine("===================================");
				Console.Write("Enter your choice: ");

				if (int.TryParse(Console.ReadLine(), out int choice))
				{
					switch (choice)
					{
						case 1:
							await DisplayAllCars();
							break;
						case 2:
							await FindCarById();
							break;
						case 3:
							await SearchCarsByModel();
							break;
						case 4:
							await AddNewCar();
							break;
						case 5:
							await UpdateCar();
							break;
						case 6:
							await DeleteCar();
							break;
						case 0:
							exit = true;
							break;
						default:
							Console.WriteLine("Invalid choice. Please try again.");
							break;
					}
				}
				else
				{
					Console.WriteLine("Invalid input. Please enter a number.");
				}

				if (!exit)
				{
					Console.WriteLine("\nPress any key to continue...");
					Console.ReadKey();
				}
			}
		}

		public async Task DisplayAllCars()
		{
			Console.Clear();
			Console.WriteLine("========== All Cars ==========");

			var cars = await _carService.GetAllCarsAsync();
			if (!cars.Any())
			{
				Console.WriteLine("No cars found in the database.");
				return;
			}

			foreach (var car in cars)
			{
				await DisplayCarDetails(car);
				Console.WriteLine("---------------------------");
			}
		}

		public async Task FindCarById()
		{
			Console.Clear();
			Console.WriteLine("========== Find Car by ID ==========");
			Console.Write("Enter Car ID: ");

			if (int.TryParse(Console.ReadLine(), out int id))
			{
				var car = await _carService.GetCarByIdAsync(id);
				if (car != null)
				{
					await DisplayCarDetails(car);
				}
				else
				{
					Console.WriteLine($"Car with ID {id} not found.");
				}
			}
			else
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
			}
		}

		public async Task SearchCarsByModel()
		{
			Console.Clear();
			Console.WriteLine("========== Search Cars by Model ==========");
			Console.Write("Enter model name (or part of model name): ");
			string searchTerm = Console.ReadLine() ?? string.Empty;

			var cars = await _carService.GetCarsByModelAsync(searchTerm);
			if (!cars.Any())
			{
				Console.WriteLine($"No cars found matching '{searchTerm}'.");
				return;
			}

			foreach (var car in cars)
			{
				await DisplayCarDetails(car);
				Console.WriteLine("---------------------------");
			}
		}

		public async Task AddNewCar()
		{
			Console.Clear();
			Console.WriteLine("========== Add New Car ==========");

			try
			{
				var car = new Car();

				Console.Write("Enter Model: ");
				car.Model = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrWhiteSpace(car.Model))
				{
					Console.WriteLine("Model is required.");
					return;
				}

				Console.Write("Enter Year (1900-2026): ");
				if (int.TryParse(Console.ReadLine(), out int year) && year >= 1900 && year <= 2026)
				{
					car.Year = year;
				}
				else
				{
					Console.WriteLine("Invalid year. Must be between 1900 and 2026.");
					return;
				}

				Console.Write("Enter Price: ");
				if (double.TryParse(Console.ReadLine(), out double price) && price >= 1000 && price <= 10000000)
				{
					car.Price = price;
				}
				else
				{
					Console.WriteLine("Invalid price. Must be between 1,000 and 10,000,000.");
					return;
				}

				Console.Write("Enter Engine Type (optional): ");
				car.EngineType = Console.ReadLine();

				Console.Write("Enter Mileage (optional): ");
				if (int.TryParse(Console.ReadLine(), out int mileage))
				{
					car.Mileage = mileage;
				}

				Console.Write("Enter Color (optional): ");
				car.Color = Console.ReadLine();

				car.IsAvailable = true;

				// Show available brands
				var brands = await _brandService.GetAllBrandsAsync();
				Console.WriteLine("\nAvailable Brands:");
				foreach (var brand in brands)
				{
					Console.WriteLine($"{brand.Id}. {brand.Name}");
				}

				Console.Write("\nEnter Brand ID: ");
				if (int.TryParse(Console.ReadLine(), out int brandId))
				{
					var brand = await _brandService.GetBrandByIdAsync(brandId);
					if (brand == null)
					{
						Console.WriteLine("Invalid Brand ID.");
						return;
					}
					car.BrandId = brandId;
				}
				else
				{
					Console.WriteLine("Invalid Brand ID format.");
					return;
				}

				var createdCar = await _carService.CreateCarAsync(car);
				Console.WriteLine($"Car added successfully with ID: {createdCar.Id}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding car: {ex.Message}");
			}
		}

		public async Task UpdateCar()
		{
			Console.Clear();
			Console.WriteLine("========== Update Car ==========");
			Console.Write("Enter Car ID to update: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
				return;
			}

			var existingCar = await _carService.GetCarByIdAsync(id);
			if (existingCar == null)
			{
				Console.WriteLine($"Car with ID {id} not found.");
				return;
			}

			await DisplayCarDetails(existingCar);
			Console.WriteLine("\nEnter new details (press Enter to keep current values):");

			Console.Write($"Model ({existingCar.Model}): ");
			string model = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(model))
			{
				existingCar.Model = model;
			}

			Console.Write($"Year ({existingCar.Year}): ");
			string yearInput = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(yearInput) && int.TryParse(yearInput, out int year) && year >= 1900 && year <= 2026)
			{
				existingCar.Year = year;
			}

			Console.Write($"Price ({existingCar.Price}): ");
			string priceInput = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(priceInput) && double.TryParse(priceInput, out double price) && price >= 1000 && price <= 10000000)
			{
				existingCar.Price = price;
			}

			Console.Write($"Engine Type ({existingCar.EngineType}): ");
			string engineType = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(engineType))
			{
				existingCar.EngineType = engineType;
			}

			Console.Write($"Mileage ({existingCar.Mileage}): ");
			string mileageInput = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(mileageInput) && int.TryParse(mileageInput, out int mileage))
			{
				existingCar.Mileage = mileage;
			}

			Console.Write($"Color ({existingCar.Color}): ");
			string color = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(color))
			{
				existingCar.Color = color;
			}

			Console.Write($"Is Available ({(existingCar.IsAvailable ? "Yes" : "No")}): (Y/N) ");
			string isAvailableInput = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(isAvailableInput))
			{
				existingCar.IsAvailable = isAvailableInput.Trim().ToUpper().StartsWith("Y");
			}

			var brands = await _brandService.GetAllBrandsAsync();
			Console.WriteLine($"\nCurrent Brand ID: {existingCar.BrandId}");
			Console.WriteLine("Available Brands:");
			foreach (var brand in brands)
			{
				Console.WriteLine($"{brand.Id}. {brand.Name}");
			}

			Console.Write("\nEnter new Brand ID (or press Enter to keep current): ");
			string brandIdInput = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(brandIdInput) && int.TryParse(brandIdInput, out int brandId))
			{
				var brand = await _brandService.GetBrandByIdAsync(brandId);
				if (brand == null)
				{
					Console.WriteLine("Invalid Brand ID. Brand will remain unchanged.");
				}
				else
				{
					existingCar.BrandId = brandId;
				}
			}

			try
			{
				var updatedCar = await _carService.UpdateCarAsync(existingCar);
				Console.WriteLine($"Car with ID {updatedCar.Id} updated successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating car: {ex.Message}");
			}
		}

		public async Task DeleteCar()
		{
			Console.Clear();
			Console.WriteLine("========== Delete Car ==========");
			Console.Write("Enter Car ID to delete: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
				return;
			}

			var car = await _carService.GetCarByIdAsync(id);
			if (car == null)
			{
				Console.WriteLine($"Car with ID {id} not found.");
				return;
			}

			await DisplayCarDetails(car);
			Console.Write("\nAre you sure you want to delete this car? (Y/N): ");
			string confirmation = Console.ReadLine() ?? string.Empty;

			if (confirmation.Trim().ToUpper().StartsWith("Y"))
			{
				bool result = await _carService.DeleteCarAsync(id);
				if (result)
				{
					Console.WriteLine($"Car with ID {id} deleted successfully.");
				}
				else
				{
					Console.WriteLine($"Failed to delete car with ID {id}.");
				}
			}
			else
			{
				Console.WriteLine("Delete operation cancelled.");
			}
		}

		public async Task DisplayCarDetails(Car car)
		{
			if (car.Brand == null && car.BrandId > 0)
			{
				car.Brand = await _brandService.GetBrandByIdAsync(car.BrandId);
			}

			Console.WriteLine($"ID: {car.Id}");
			Console.WriteLine($"Model: {car.Model}");
			Console.WriteLine($"Year: {car.Year}");
			Console.WriteLine($"Brand: {car.Brand?.Name ?? "Unknown"}");
			Console.WriteLine($"Price: ${car.Price:N2}");

			if (!string.IsNullOrEmpty(car.EngineType))
				Console.WriteLine($"Engine: {car.EngineType}");

			if (car.Mileage.HasValue)
				Console.WriteLine($"Mileage: {car.Mileage:N0} miles");

			if (!string.IsNullOrEmpty(car.Color))
				Console.WriteLine($"Color: {car.Color}");

			Console.WriteLine($"Available: {(car.IsAvailable ? "Yes" : "No")}");
		}
	}
}