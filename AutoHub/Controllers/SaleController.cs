using AutoHub.Business.Services.Interfaces;
using AutoHub.Controllers.Interfaces;
using AutoHub.Data.Models;
using AutoHub.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Controllers
{
    public class SaleController : ISaleController
    {
		private readonly ISaleService _saleService;
		private readonly ICarService _carService;
		private readonly ICustomerService _customerService;
		private readonly ISalespersonService _salespersonService;
		private readonly SaleView _saleView;

		public SaleController(
			ISaleService saleService,
			ICarService carService,
			ICustomerService customerService,
			ISalespersonService salespersonService)
		{
			_saleService = saleService;
			_carService = carService;
			_customerService = customerService;
			_salespersonService = salespersonService;
			_saleView = new SaleView(saleService, carService, customerService, salespersonService);
		}

		public async Task Run()
		{
			await _saleView.DisplayMenu();
		}

		public async Task<IEnumerable<Sale>> GetAllSalesAsync()
		{
			return await _saleService.GetAllSalesAsync();
		}

		public async Task<Sale> GetSaleByIdAsync(int id)
		{
			return await _saleService.GetSaleByIdAsync(id);
		}

		public async Task<IEnumerable<Sale>> SearchSalesByCarModelAsync(string searchTerm)
		{
			return await _saleService.GetSalesByCarModelAsync(searchTerm);
		}

		public async Task<Sale> CreateSaleAsync(Sale sale)
		{
			// Validate incoming sale
			if (sale.SaleDate > DateTime.Now)
			{
				throw new ArgumentException("Sale date cannot be in the future.");
			}

			if (sale.SalePrice <= 0 || sale.SalePrice > 15000000)
			{
				throw new ArgumentException("Sale price must be between 0.01 and 15,000,000.");
			}

			var car = await _carService.GetCarByIdAsync(sale.CarId);
			if (car == null)
			{
				throw new ArgumentException($"Car with ID {sale.CarId} does not exist.");
			}

			if (!car.IsAvailable)
			{
				throw new ArgumentException($"Car with ID {sale.CarId} is not available for sale.");
			}

			var customer = await _customerService.GetCustomerByIdAsync(sale.CustomerId);
			if (customer == null)
			{
				throw new ArgumentException($"Customer with ID {sale.CustomerId} does not exist.");
			}

			var salesperson = await _salespersonService.GetSalespersonByIdAsync(sale.SalespersonId);
			if (salesperson == null)
			{
				throw new ArgumentException($"Salesperson with ID {sale.SalespersonId} does not exist.");
			}

			var createdSale = await _saleService.CreateSaleAsync(sale);

			car.IsAvailable = false;
			await _carService.UpdateCarAsync(car);

			return createdSale;
		}

		public async Task<Sale> UpdateSaleAsync(Sale sale)
		{
			var existingSale = await _saleService.GetSaleByIdAsync(sale.Id);
			// Validate existing and incoming sale
			if (existingSale == null)
			{
				throw new KeyNotFoundException($"Sale with ID {sale.Id} not found.");
			}

			if (sale.SaleDate > DateTime.Now)
			{
				throw new ArgumentException("Sale date cannot be in the future.");
			}

			if (sale.SalePrice <= 0 || sale.SalePrice > 15000000)
			{
				throw new ArgumentException("Sale price must be between 0.01 and 15,000,000.");
			}

			if (sale.CarId != existingSale.CarId)
			{
				var oldCar = await _carService.GetCarByIdAsync(existingSale.CarId);
				if (oldCar != null)
				{
					oldCar.IsAvailable = true;
					await _carService.UpdateCarAsync(oldCar);
				}

				var newCar = await _carService.GetCarByIdAsync(sale.CarId);
				if (newCar == null)
				{
					throw new ArgumentException($"Car with ID {sale.CarId} does not exist.");
				}

				if (!newCar.IsAvailable)
				{
					throw new ArgumentException($"Car with ID {sale.CarId} is not available for sale.");
				}

				newCar.IsAvailable = false;
				await _carService.UpdateCarAsync(newCar);
			}

			if (sale.CustomerId != existingSale.CustomerId)
			{
				var customer = await _customerService.GetCustomerByIdAsync(sale.CustomerId);
				if (customer == null)
				{
					throw new ArgumentException($"Customer with ID {sale.CustomerId} does not exist.");
				}
			}

			if (sale.SalespersonId != existingSale.SalespersonId)
			{
				var salesperson = await _salespersonService.GetSalespersonByIdAsync(sale.SalespersonId);
				if (salesperson == null)
				{
					throw new ArgumentException($"Salesperson with ID {sale.SalespersonId} does not exist.");
				}
			}

			return await _saleService.UpdateSaleAsync(sale);
		}

		public async Task<bool> DeleteSaleAsync(int id)
		{
			var existingSale = await _saleService.GetSaleByIdAsync(id);

			// Validate existing sale
			if (existingSale == null)
			{
				return false;
			}

			var car = await _carService.GetCarByIdAsync(existingSale.CarId);
			if (car != null)
			{
				car.IsAvailable = true;
				await _carService.UpdateCarAsync(car);
			}

			return await _saleService.DeleteSaleAsync(id);
		}

		public async Task<IEnumerable<Car>> GetAvailableCarsAsync()
		{
			var allCars = await _carService.GetAllCarsAsync();
			return allCars.Where(c => c.IsAvailable);
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			return await _customerService.GetAllCustomersAsync();
		}

		public async Task<IEnumerable<Salesperson>> GetAllSalespersonsAsync()
		{
			return await _salespersonService.GetAllSalespersonAsync();
		}

	}
}
