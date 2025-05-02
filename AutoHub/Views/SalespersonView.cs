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
    public class SalespersonView : ISalespersonView
    {
		private readonly ISalespersonService _salespersonService;

		public SalespersonView(ISalespersonService salespersonService)
		{
			_salespersonService = salespersonService;
		}

		public async Task DisplayMenu()
		{
			bool exit = false;
			while (!exit)
			{
				Console.Clear();
				Console.WriteLine("========== Salesperson Management ==========");
				Console.WriteLine("1. View All Salespersons");
				Console.WriteLine("2. Find Salesperson by ID");
				Console.WriteLine("3. Search Salespersons by First Name");
				Console.WriteLine("4. Add New Salesperson");
				Console.WriteLine("5. Update Salesperson");
				Console.WriteLine("6. Delete Salesperson");
				Console.WriteLine("0. Back to Main Menu");
				Console.WriteLine("==========================================");
				Console.Write("Enter your choice: ");

				if (int.TryParse(Console.ReadLine(), out int choice))
				{
					switch (choice)
					{
						case 1:
							await DisplayAllSalespersons();
							break;
						case 2:
							await FindSalespersonById();
							break;
						case 3:
							await SearchSalespersonsByFirstName();
							break;
						case 4:
							await AddNewSalesperson();
							break;
						case 5:
							await UpdateSalesperson();
							break;
						case 6:
							await DeleteSalesperson();
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

		public async Task DisplayAllSalespersons()
		{
			Console.Clear();
			Console.WriteLine("========== All Salespersons ==========");

			var salespersons = await _salespersonService.GetAllSalespersonAsync();
			if (!salespersons.Any())
			{
				Console.WriteLine("No salespersons found in the database.");
				return;
			}

			foreach (var salesperson in salespersons)
			{
				await DisplaySalespersonDetails(salesperson);
				Console.WriteLine("---------------------------");
			}
		}

		public async Task FindSalespersonById()
		{
			Console.Clear();
			Console.WriteLine("========== Find Salesperson by ID ==========");
			Console.Write("Enter Salesperson ID: ");

			if (int.TryParse(Console.ReadLine(), out int id))
			{
				var salesperson = await _salespersonService.GetSalespersonByIdAsync(id);
				if (salesperson != null)
				{
					await DisplaySalespersonDetails(salesperson);
				}
				else
				{
					Console.WriteLine($"Salesperson with ID {id} not found.");
				}
			}
			else
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
			}
		}

		public async Task SearchSalespersonsByFirstName()
		{
			Console.Clear();
			Console.WriteLine("========== Search Salespersons by First Name ==========");
			Console.Write("Enter first name (or part of first name): ");
			string searchTerm = Console.ReadLine() ?? string.Empty;

			var salespersons = await _salespersonService.GetSalespersonByFirstNameAsync(searchTerm);
			if (!salespersons.Any())
			{
				Console.WriteLine($"No salespersons found matching '{searchTerm}'.");
				return;
			}

			foreach (var salesperson in salespersons)
			{
				await DisplaySalespersonDetails(salesperson);
				Console.WriteLine("---------------------------");
			}
		}

		public async Task AddNewSalesperson()
		{
			Console.Clear();
			Console.WriteLine("========== Add New Salesperson ==========");

			try
			{
				var salesperson = new Salesperson();

				Console.Write("Enter First Name: ");
				salesperson.FirstName = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrWhiteSpace(salesperson.FirstName))
				{
					Console.WriteLine("First name is required.");
					return;
				}

				Console.Write("Enter Last Name: ");
				salesperson.LastName = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrWhiteSpace(salesperson.LastName))
				{
					Console.WriteLine("Last name is required.");
					return;
				}

				Console.Write("Enter Employee Number: ");
				salesperson.EmployeeNumber = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrWhiteSpace(salesperson.EmployeeNumber))
				{
					Console.WriteLine("Employee number is required.");
					return;
				}

				Console.Write("Enter Hire Date (yyyy-MM-dd): ");
				if (DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
				{
					salesperson.HireDate = hireDate;
				}
				else
				{
					Console.WriteLine("Invalid date format.");
					return;
				}

				var createdSalesperson = await _salespersonService.CreateSalespersonAsync(salesperson);
				Console.WriteLine($"Salesperson added successfully with ID: {createdSalesperson.Id}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding salesperson: {ex.Message}");
			}
		}

		public async Task UpdateSalesperson()
		{
			Console.Clear();
			Console.WriteLine("========== Update Salesperson ==========");
			Console.Write("Enter Salesperson ID to update: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
				return;
			}

			var existingSalesperson = await _salespersonService.GetSalespersonByIdAsync(id);
			if (existingSalesperson == null)
			{
				Console.WriteLine($"Salesperson with ID {id} not found.");
				return;
			}

			await DisplaySalespersonDetails(existingSalesperson);
			Console.WriteLine("\nEnter new details (press Enter to keep current values):");

			Console.Write($"First Name ({existingSalesperson.FirstName}): ");
			string firstName = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(firstName))
			{
				existingSalesperson.FirstName = firstName;
			}

			Console.Write($"Last Name ({existingSalesperson.LastName}): ");
			string lastName = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(lastName))
			{
				existingSalesperson.LastName = lastName;
			}

			Console.Write($"Employee Number ({existingSalesperson.EmployeeNumber}): ");
			string employeeNumber = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(employeeNumber))
			{
				existingSalesperson.EmployeeNumber = employeeNumber;
			}

			Console.Write($"Hire Date ({existingSalesperson.HireDate:yyyy-MM-dd}): ");
			string hireDateInput = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(hireDateInput) && DateTime.TryParse(hireDateInput, out DateTime hireDate))
			{
				existingSalesperson.HireDate = hireDate;
			}

			try
			{
				var updatedSalesperson = await _salespersonService.UpdateSalespersonAsync(existingSalesperson);
				Console.WriteLine($"Salesperson with ID {updatedSalesperson.Id} updated successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating salesperson: {ex.Message}");
			}
		}

		public async Task DeleteSalesperson()
		{
			Console.Clear();
			Console.WriteLine("========== Delete Salesperson ==========");
			Console.Write("Enter Salesperson ID to delete: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
				return;
			}

			var salesperson = await _salespersonService.GetSalespersonByIdAsync(id);
			if (salesperson == null)
			{
				Console.WriteLine($"Salesperson with ID {id} not found.");
				return;
			}

			await DisplaySalespersonDetails(salesperson);
			Console.Write("\nAre you sure you want to delete this salesperson? (Y/N): ");
			string confirmation = Console.ReadLine() ?? string.Empty;

			if (confirmation.Trim().ToUpper().StartsWith("Y"))
			{
				bool result = await _salespersonService.DeleteSalespersonAsync(id);
				if (result)
				{
					Console.WriteLine($"Salesperson with ID {id} deleted successfully.");
				}
				else
				{
					Console.WriteLine($"Failed to delete salesperson with ID {id}.");
				}
			}
			else
			{
				Console.WriteLine("Delete operation cancelled.");
			}
		}

		public async Task DisplaySalespersonDetails(Salesperson salesperson)
		{
			Console.WriteLine($"ID: {salesperson.Id}");
			Console.WriteLine($"Name: {salesperson.FirstName} {salesperson.LastName}");
			Console.WriteLine($"Employee Number: {salesperson.EmployeeNumber}");
			Console.WriteLine($"Hire Date: {salesperson.HireDate:yyyy-MM-dd}");
		}
	}
}
