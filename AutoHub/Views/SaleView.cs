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
	public class SaleView : ISaleView
	{
		private readonly ISaleService _saleService;
		private readonly ICarService _carService;
		private readonly ICustomerService _customerService;
		private readonly ISalespersonService _salespersonService;

		public SaleView(ISaleService saleService, ICarService carService, ICustomerService customerService, ISalespersonService salespersonService)
		{
			_saleService = saleService;
			_carService = carService;
			_customerService = customerService;
			_salespersonService = salespersonService;
		}

		public async Task DisplayMenu()
		{
            //	Main menu loop
            bool exit = false;
			while (!exit)
			{
				Console.Clear();
				Console.WriteLine("========== Sale Management ==========");
				Console.WriteLine("1. View All Sales");
				Console.WriteLine("2. Find Sale by ID");
				Console.WriteLine("3. Search Sales by Car Model");
				Console.WriteLine("4. Add New Sale");
				Console.WriteLine("5. Update Sale");
				Console.WriteLine("6. Delete Sale");
				Console.WriteLine("0. Back to Main Menu");
				Console.WriteLine("===================================");
				Console.Write("Enter your choice: ");

				if (int.TryParse(Console.ReadLine(), out int choice))
				{
					switch (choice)
					{
						case 1:
							await DisplayAllSales();
							break;
						case 2:
							await FindSaleById();
							break;
						case 3:
							await SearchSalesByCarModel();
							break;
						case 4:
							await AddNewSale();
							break;
						case 5:
							await UpdateSale();
							break;
						case 6:
							await DeleteSale();
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

		public async Task DisplayAllSales()
		{
            Console.Clear();
			Console.WriteLine("========== All Sales ==========");

            // Fetch all sales from the service
            var sales = await _saleService.GetAllSalesAsync();
			if (!sales.Any())
			{
				Console.WriteLine("No sales found in the database.");
				return;
			}

			foreach (var sale in sales)
			{
				await DisplaySaleDetails(sale);
				Console.WriteLine("---------------------------");
			}
		}

		public async Task FindSaleById()
		{
			Console.Clear();
			Console.WriteLine("========== Find Sale by ID ==========");
			Console.Write("Enter Sale ID: ");

            // Validate user input
            if (int.TryParse(Console.ReadLine(), out int id))
			{
				var sale = await _saleService.GetSaleByIdAsync(id);
				if (sale != null)
				{
					await DisplaySaleDetails(sale);
				}
				else
				{
					Console.WriteLine($"Sale with ID {id} not found.");
				}
			}
			else
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
			}
		}

		public async Task SearchSalesByCarModel()
		{
			Console.Clear();
			Console.WriteLine("========== Search Sales by Car Model ==========");
			Console.Write("Enter car model name (or part of model name): ");
			string searchTerm = Console.ReadLine() ?? string.Empty;

            // Validate user input
            var sales = await _saleService.GetSalesByCarModelAsync(searchTerm);
			if (!sales.Any())
			{
				Console.WriteLine($"No sales found for cars matching '{searchTerm}'.");
				return;
			}

            //	Loop through each sale and display their details
            foreach (var sale in sales)
			{
				await DisplaySaleDetails(sale);
				Console.WriteLine("---------------------------");
			}
		}

		public async Task AddNewSale()
		{
			Console.Clear();
			Console.WriteLine("========== Add New Sale ==========");

            // Validate user input
            try
            {
				var sale = new Sale();

				sale.SaleDate = DateTime.Now;
				Console.WriteLine($"Sale Date: {sale.SaleDate.ToShortDateString()}");

				var cars = await _carService.GetAllCarsAsync();
				var availableCars = cars.Where(c => c.IsAvailable).ToList();

                // Check if there are any available cars
                if (!availableCars.Any())
				{
					Console.WriteLine("No cars available for sale.");
					return;
				}

                // Display available cars
                Console.WriteLine("\nAvailable Cars:");
				foreach (var c in availableCars)
				{
					Console.WriteLine($"{c.Id}. {c.Year} {c.Brand?.Name} {c.Model} - ${c.Price:N2}");
				}

                // Prompt the user to select a car
                Console.Write("\nEnter Car ID: ");
				if (int.TryParse(Console.ReadLine(), out int carId))
				{
					var selectedCar = availableCars.FirstOrDefault(c => c.Id == carId);
					if (selectedCar == null)
					{
						Console.WriteLine("Invalid Car ID or car is not available.");
						return;
					}
					sale.CarId = carId;

					Console.Write($"Enter Sale Price (suggested: ${selectedCar.Price:N2}): ");
				}
				else
				{
					Console.WriteLine("Invalid Car ID format.");
					return;
				}

                // Validate sale price input
                if (decimal.TryParse(Console.ReadLine(), out decimal salePrice) && salePrice > 0 && salePrice <= 15000000)
				{
					sale.SalePrice = salePrice;
				}
				else
				{
					Console.WriteLine("Invalid sale price. Must be greater than 0 and not exceed 15,000,000.");
					return;
				}

                // Prompt the user to select a customer
                var customers = await _customerService.GetAllCustomersAsync();
				Console.WriteLine("\nCustomers:");
				foreach (var customer in customers)
				{
					Console.WriteLine($"{customer.Id}. {customer.FirstName} {customer.LastName} - {customer.Email}");
				}

				Console.Write("\nEnter Customer ID: ");
				if (int.TryParse(Console.ReadLine(), out int customerId))
				{
					var customer = await _customerService.GetCustomerByIdAsync(customerId);
					if (customer == null)
					{
						Console.WriteLine("Invalid Customer ID.");
						return;
					}
					sale.CustomerId = customerId;
				}
				else
				{
					Console.WriteLine("Invalid Customer ID format.");
					return;
				}

                // Prompt the user to select a salesperson
                var salespersons = await _salespersonService.GetAllSalespersonAsync();
				Console.WriteLine("\nSalespersons:");
				foreach (var salesperson in salespersons)
				{
					Console.WriteLine($"{salesperson.Id}. {salesperson.FirstName} {salesperson.LastName} (#{salesperson.EmployeeNumber})");
				}

                Console.Write("\nEnter Salesperson ID: ");
				if (int.TryParse(Console.ReadLine(), out int salespersonId))
				{
					var salesperson = await _salespersonService.GetSalespersonByIdAsync(salespersonId);
					if (salesperson == null)
					{
						Console.WriteLine("Invalid Salesperson ID.");
						return;
					}
					sale.SalespersonId = salespersonId;
				}
				else
				{
					Console.WriteLine("Invalid Salesperson ID format.");
					return;
				}

                // Create the sale
                var createdSale = await _saleService.CreateSaleAsync(sale);
				Console.WriteLine($"Sale added successfully with ID: {createdSale.Id}");

				var car = await _carService.GetCarByIdAsync(sale.CarId);
				if (car != null)
				{
					car.IsAvailable = false;
					await _carService.UpdateCarAsync(car);
				}
			}
            // Catch any exceptions that occur during the process
            catch (Exception ex)
			{
				Console.WriteLine($"Error adding sale: {ex.Message}");
			}
		}

		public async Task UpdateSale()
		{
			Console.Clear();
			await DisplayAllSales();
			Console.WriteLine("========== Update Sale ==========");
			Console.Write("Enter Sale ID to update: ");

            // Validate user input
            if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
				return;
			}

            // Fetch the existing sale
            var existingSale = await _saleService.GetSaleByIdAsync(id);
			if (existingSale == null)
			{
				Console.WriteLine($"Sale with ID {id} not found.");
				return;
			}

            // Display existing sale details
            await DisplaySaleDetails(existingSale);
			Console.WriteLine("\nEnter new details (press Enter to keep current values):");

            // Prompt the user to update the sale date
            Console.Write($"Sale Date ({existingSale.SaleDate.ToShortDateString()}): ");
			string dateInput = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime saleDate))
			{
				existingSale.SaleDate = saleDate;
			}

            // Prompt the user to update the sale price
            Console.Write($"Sale Price (${existingSale.SalePrice:N2}): ");
			string priceInput = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal salePrice) && salePrice > 0 && salePrice <= 15000000)
			{
				existingSale.SalePrice = salePrice;
			}

            // Prompt the user to change the car
            bool changingCar = false;
			Console.Write($"Do you want to change the car? Current Car ID: {existingSale.CarId} (Y/N): ");
			string changeCar = Console.ReadLine() ?? string.Empty;
			if (changeCar.Trim().ToUpper().StartsWith("Y"))
			{
				changingCar = true;

				var cars = await _carService.GetAllCarsAsync();
				var availableCars = cars.Where(c => c.IsAvailable || c.Id == existingSale.CarId).ToList();

                // Display available cars
                Console.WriteLine("\nAvailable Cars:");
				foreach (var car in availableCars)
				{
					Console.WriteLine($"{car.Id}. {car.Year} {car.Brand?.Name} {car.Model} - ${car.Price:N2}" +
						(car.Id == existingSale.CarId ? " (Current)" : ""));
				}

                // Prompt the user to select a new car
                Console.Write("\nEnter new Car ID: ");
				string carIdInput = Console.ReadLine() ?? string.Empty;
				if (!string.IsNullOrWhiteSpace(carIdInput) && int.TryParse(carIdInput, out int carId))
				{
					var selectedCar = availableCars.FirstOrDefault(c => c.Id == carId);
					if (selectedCar == null)
					{
						Console.WriteLine("Invalid Car ID or car is not available. Car will remain unchanged.");
					}
					else
					{
						existingSale.CarId = carId;
					}
				}
			}

            // Prompt the user to change the customer
            Console.Write($"Do you want to change the customer? Current Customer ID: {existingSale.CustomerId} (Y/N): ");
			string changeCustomer = Console.ReadLine() ?? string.Empty;
			if (changeCustomer.Trim().ToUpper().StartsWith("Y"))
			{
                // Display existing customer details
                var customers = await _customerService.GetAllCustomersAsync();
				Console.WriteLine("\nCustomers:");
				foreach (var customer in customers)
				{
                    Console.WriteLine($"{customer.Id}. {customer.FirstName} {customer.LastName} - {customer.Email}" +
						(customer.Id == existingSale.CustomerId ? " (Current)" : ""));
				}

                // Prompt the user to select a new customer
                Console.Write("\nEnter new Customer ID: ");
				string customerIdInput = Console.ReadLine() ?? string.Empty;
				if (!string.IsNullOrWhiteSpace(customerIdInput) && int.TryParse(customerIdInput, out int customerId))
				{
                    // Fetch the customer by ID
                    var customer = await _customerService.GetCustomerByIdAsync(customerId);
					if (customer == null)
					{
						Console.WriteLine("Invalid Customer ID. Customer will remain unchanged.");
					}
					else
					{
						existingSale.CustomerId = customerId;
					}
				}
			}

            // Prompt the user to change the salesperson
            Console.Write($"Do you want to change the salesperson? Current Salesperson ID: {existingSale.SalespersonId} (Y/N): ");
			string changeSalesperson = Console.ReadLine() ?? string.Empty;
			if (changeSalesperson.Trim().ToUpper().StartsWith("Y"))
			{
                // Display existing salesperson details
                var salespersons = await _salespersonService.GetAllSalespersonAsync();
				Console.WriteLine("\nSalespersons:");
				foreach (var salesperson in salespersons)
				{
					Console.WriteLine($"{salesperson.Id}. {salesperson.FirstName} {salesperson.LastName} (#{salesperson.EmployeeNumber})" +
						(salesperson.Id == existingSale.SalespersonId ? " (Current)" : ""));
				}

                // Prompt the user to select a new salesperson
                Console.Write("\nEnter new Salesperson ID: ");
				string salespersonIdInput = Console.ReadLine() ?? string.Empty;
				if (!string.IsNullOrWhiteSpace(salespersonIdInput) && int.TryParse(salespersonIdInput, out int salespersonId))
				{
                    // Fetch the salesperson by ID
                    var salesperson = await _salespersonService.GetSalespersonByIdAsync(salespersonId);
					if (salesperson == null)
					{
						Console.WriteLine("Invalid Salesperson ID. Salesperson will remain unchanged.");
					}
					else
					{
						existingSale.SalespersonId = salespersonId;
					}
				}
			}
            // Confirm the changes
            try
            {
				var updatedSale = await _saleService.UpdateSaleAsync(existingSale);
				Console.WriteLine($"Sale with ID {updatedSale.Id} updated successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating sale: {ex.Message}");
			}
		}

		public async Task DeleteSale()
		{
			Console.Clear();
			await DisplayAllSales();
			Console.WriteLine("========== Delete Sale ==========");
			Console.Write("Enter Sale ID to delete: ");

            // Validate user input
            if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
				return;
			}

            // Fetch the existing sale
            var sale = await _saleService.GetSaleByIdAsync(id);
			if (sale == null)
			{
				Console.WriteLine($"Sale with ID {id} not found.");
				return;
			}

            // Display existing sale details
            await DisplaySaleDetails(sale);
			Console.Write("\nAre you sure you want to delete this sale? (Y/N): ");
			string confirmation = Console.ReadLine() ?? string.Empty;

			if (confirmation.Trim().ToUpper().StartsWith("Y"))
			{
                // Delete the sale
                bool result = await _saleService.DeleteSaleAsync(id);
				if (result)
				{
					Console.WriteLine($"Sale with ID {id} deleted successfully.");

					var car = await _carService.GetCarByIdAsync(sale.CarId);
					if (car != null)
					{
						car.IsAvailable = true;
						await _carService.UpdateCarAsync(car);
						Console.WriteLine($"Car with ID {car.Id} is now available for sale again.");
					}
				}
				else
				{
					Console.WriteLine($"Failed to delete sale with ID {id}.");
				}
			}
			else
			{
				Console.WriteLine("Delete operation cancelled.");
			}
		}

		public async Task DisplaySaleDetails(Sale sale)
		{
            // Display detailed information about a sale
            if (sale.Car == null && sale.CarId > 0)
			{
				sale.Car = await _carService.GetCarByIdAsync(sale.CarId);
			}
			if (sale.Customer == null && sale.CustomerId > 0)
			{
				sale.Customer = await _customerService.GetCustomerByIdAsync(sale.CustomerId);
			}
			if (sale.Salesperson == null && sale.SalespersonId > 0)
			{
				sale.Salesperson = await _salespersonService.GetSalespersonByIdAsync(sale.SalespersonId);
			}

			Console.WriteLine($"Sale ID: {sale.Id}");
			Console.WriteLine($"Date: {sale.SaleDate.ToShortDateString()}");
			Console.WriteLine($"Sale Price: ${sale.SalePrice:N2}");

            // Display car, customer, and salesperson details
            if (sale.Car != null)
			{
				Console.WriteLine($"Car: {sale.Car.Year} {sale.Car?.Brand?.Name} {sale.Car?.Model} (ID: {sale.CarId})");
			}
			else
			{
				Console.WriteLine($"Car ID: {sale.CarId} (Car information not available)");
			}

			if (sale.Customer != null)
			{
				Console.WriteLine($"Customer: {sale.Customer.FirstName} {sale.Customer.LastName} (ID: {sale.CustomerId})");
				Console.WriteLine($"Customer Email: {sale.Customer.Email}");
			}
			else
			{
				Console.WriteLine($"Customer ID: {sale.CustomerId} (Customer information not available)");
			}

            // Display salesperson details
            if (sale.Salesperson != null)
			{
				Console.WriteLine($"Salesperson: {sale.Salesperson.FirstName} {sale.Salesperson.LastName} (ID: {sale.SalespersonId})");
				Console.WriteLine($"Employee Number: #{sale.Salesperson.EmployeeNumber}");
			}
			else
			{
				Console.WriteLine($"Salesperson ID: {sale.SalespersonId} (Salesperson information not available)");
			}
		}
	}
}