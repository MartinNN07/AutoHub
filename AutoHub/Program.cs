using AutoHub.Business.Services;
using AutoHub.Business.Services.Interfaces;
using AutoHub.Controllers;
using AutoHub.Controllers.Interfaces;
using AutoHub.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AutoHub
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			// Setup dependency injection
			var serviceProvider = ConfigureServices();

			// Run the main menu
			await RunMainMenu(serviceProvider);
		}

		static ServiceProvider ConfigureServices()
		{
			// Create service collection
			var services = new ServiceCollection();

			// Configure database connection
			services.AddDbContext<AutoHubDbContext>(options =>
				options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AutoHubDb;Trusted_Connection=True;MultipleActiveResultSets=true"));

			// Register business services
			services.AddScoped<IBrandService, BrandService>();
			services.AddScoped<ICarService, CarService>();
			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<ISaleService, SaleService>();
			services.AddScoped<ISalespersonService, SalespersonService>();

			// Register controllers
			services.AddScoped<ICarController, CarController>();
			services.AddScoped<IBrandController, BrandController>();
			services.AddScoped<ICustomerController, CustomerController>();
			services.AddScoped<ISaleController, SaleController>();
			services.AddScoped<ISalespersonController, SalespersonController>();
			// Add other controllers as needed when they're implemented

			return services.BuildServiceProvider();
		}

		static async Task RunMainMenu(ServiceProvider serviceProvider)
		{
			bool exit = false;

			while (!exit)
			{
				Console.Clear();
				Console.WriteLine("========== AutoHub Management System ==========");
				Console.WriteLine("1. Car Management");
				Console.WriteLine("2. Customer Management");
				Console.WriteLine("3. Salesperson Management");
				Console.WriteLine("4. Sales Management");
				Console.WriteLine("5. Brand Management");
				Console.WriteLine("0. Exit");
				Console.WriteLine("==============================================");
				Console.Write("Enter your choice: ");

				if (int.TryParse(Console.ReadLine(), out int choice))
				{
					switch (choice)
					{
						case 1:
							await ManageCars(serviceProvider);
							break;
						case 2:
							await ManageCustomers(serviceProvider);
							break;
						case 3:
							await ManageSalespersons(serviceProvider);
							break;
						case 4:
							await ManageSales(serviceProvider);
							break;
						case 5:
							await ManageBrands(serviceProvider);
							break;
						case 0:
							exit = true;
							break;
						default:
							Console.WriteLine("Invalid choice. Please try again.");
							await Task.Delay(1500);
							break;
					}
				}
				else
				{
					Console.WriteLine("Invalid input. Please enter a number.");
					await Task.Delay(1500);
				}
			}

			Console.WriteLine("Thank you for using AutoHub Management System!");
		}

		static async Task ManageCars(ServiceProvider serviceProvider)
		{
			var carController = serviceProvider.GetRequiredService<ICarController>();
			await carController.Run();
		}
		static async Task ManageCustomers(ServiceProvider serviceProvider)
		{
			var customerController = serviceProvider.GetRequiredService<ICustomerController>();
			await customerController.Run();
		}

		static async Task ManageSales(ServiceProvider serviceProvider)
		{
			var saleController = serviceProvider.GetRequiredService<ISaleController>();
			await saleController.Run();
		}
		static async Task ManageSalespersons(ServiceProvider serviceProvider)
		{
			var salespersonController = serviceProvider.GetRequiredService<ISalespersonController>();
			await salespersonController.Run();
		}
		static async Task ManageBrands(ServiceProvider serviceProvider)
		{
			var brandController = serviceProvider.GetRequiredService<IBrandController>();
			await brandController.Run();
		}
	}
}