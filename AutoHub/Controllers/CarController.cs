using AutoHub.Business.Services;
using AutoHub.Business.Services.Interfaces;
using AutoHub.Controllers.Interfaces;
using AutoHub.Data.Models;
using AutoHub.Views;
using AutoHub.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers
{
	public class CarController : ICarController
	{
		private readonly ICarService _carService;
		private readonly IBrandService _brandService;
		private readonly CarView _carView;

		public CarController(ICarService carService, IBrandService brandService)
		{
			_carService = carService;
			_brandService = brandService;
			_carView = new CarView(carService, brandService);
		}

		public async Task Run()
		{
			await _carView.DisplayMenu();
		}

		public async Task<IEnumerable<Car>> GetAllCarsAsync()
		{
			return await _carService.GetAllCarsAsync();
		}

		public async Task<Car> GetCarByIdAsync(int id)
		{
			return await _carService.GetCarByIdAsync(id);
		}

		public async Task<IEnumerable<Car>> SearchCarsByModelAsync(string searchTerm)
		{
			return await _carService.GetCarsByModelAsync(searchTerm);
		}

		public async Task<Car> CreateCarAsync(Car car)
		{
			var brand = await _brandService.GetBrandByIdAsync(car.BrandId);
			if (brand == null)
			{
				throw new ArgumentException($"Brand with ID {car.BrandId} does not exist.");
			}

			if (string.IsNullOrWhiteSpace(car.Model))
			{
				throw new ArgumentException("Car model is required.");
			}

			if (car.Year < 1900 || car.Year > 2026)
			{
				throw new ArgumentException("Year must be between 1900 and 2026.");
			}

			if (car.Price < 1000 || car.Price > 10000000)
			{
				throw new ArgumentException("Price must be between 1,000 and 10,000,000.");
			}

			if (car.Mileage.HasValue && (car.Mileage < 0 || car.Mileage > 2000000))
			{
				throw new ArgumentException("Mileage must be between 0 and 2,000,000.");
			}

			return await _carService.CreateCarAsync(car);
		}

		public async Task<Car> UpdateCarAsync(Car car)
		{
			var existingCar = await _carService.GetCarByIdAsync(car.Id);
			if (existingCar == null)
			{
				throw new KeyNotFoundException($"Car with ID {car.Id} not found.");
			}

			if (car.BrandId != existingCar.BrandId)
			{
				var brand = await _brandService.GetBrandByIdAsync(car.BrandId);
				if (brand == null)
				{
					throw new ArgumentException($"Brand with ID {car.BrandId} does not exist.");
				}
			}

			if (string.IsNullOrWhiteSpace(car.Model))
			{
				throw new ArgumentException("Car model is required.");
			}

			if (car.Year < 1900 || car.Year > 2026)
			{
				throw new ArgumentException("Year must be between 1900 and 2026.");
			}

			if (car.Price < 1000 || car.Price > 10000000)
			{
				throw new ArgumentException("Price must be between 1,000 and 10,000,000.");
			}

			if (car.Mileage.HasValue && (car.Mileage < 0 || car.Mileage > 2000000))
			{
				throw new ArgumentException("Mileage must be between 0 and 2,000,000.");
			}

			return await _carService.UpdateCarAsync(car);
		}

		public async Task<bool> DeleteCarAsync(int id)
		{
			var existingCar = await _carService.GetCarByIdAsync(id);
			if (existingCar == null)
			{
				return false;
			}

			return await _carService.DeleteCarAsync(id);
		}

		public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
		{
			return await _brandService.GetAllBrandsAsync();
		}
	}
}
