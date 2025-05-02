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
    public class CustomerView : ICustomerView
    {
		private readonly ICustomerService _customerService;

		public CustomerView(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public async Task DisplayMenu()
		{
			bool exit = false;
			while (!exit)
			{
				Console.Clear();
				Console.WriteLine("========== Customer Management ==========");
				Console.WriteLine("1. View All Customers");
				Console.WriteLine("2. Find Customer by ID");
				Console.WriteLine("3. Search Customers by First Name");
				Console.WriteLine("4. Add New Customer");
				Console.WriteLine("5. Update Customer");
				Console.WriteLine("6. Delete Customer");
				Console.WriteLine("0. Back to Main Menu");
				Console.WriteLine("=======================================");
				Console.Write("Enter your choice: ");

				if (int.TryParse(Console.ReadLine(), out int choice))
				{
					switch (choice)
					{
						case 1:
							await DisplayAllCustomers();
							break;
						case 2:
							await FindCustomerById();
							break;
						case 3:
							await SearchCustomersByFirstName();
							break;
						case 4:
							await AddNewCustomer();
							break;
						case 5:
							await UpdateCustomer();
							break;
						case 6:
							await DeleteCustomer();
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

		public async Task DisplayAllCustomers()
		{
			Console.Clear();
			Console.WriteLine("========== All Customers ==========");

			var customers = await _customerService.GetAllCustomersAsync();
			if (!customers.Any())
			{
				Console.WriteLine("No customers found in the database.");
				return;
			}

			foreach (var customer in customers)
			{
				await DisplayCustomerDetails(customer);
				Console.WriteLine("---------------------------");
			}
		}

		public Task DisplayCustomerDetails(Customer customer)
		{
			Console.WriteLine($"ID: {customer.Id}");
			Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
			Console.WriteLine($"Email: {customer.Email ?? "N/A"}");
			Console.WriteLine($"Phone: {customer.PhoneNumber}");

			return Task.CompletedTask;
		}

		public async Task FindCustomerById()
		{
			Console.Clear();
			Console.WriteLine("========== Find Customer by ID ==========");
			Console.Write("Enter Customer ID: ");

			if (int.TryParse(Console.ReadLine(), out int id))
			{
				var customer = await _customerService.GetCustomerByIdAsync(id);
				if (customer != null)
				{
					await DisplayCustomerDetails(customer);
				}
				else
				{
					Console.WriteLine($"Customer with ID {id} not found.");
				}
			}
			else
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
			}
		}

		public async Task SearchCustomersByFirstName()
		{
			Console.Clear();
			Console.WriteLine("========== Search Customers by First Name ==========");
			Console.Write("Enter first name (or part of first name): ");
			string searchTerm = Console.ReadLine() ?? string.Empty;

			var customers = await _customerService.GetCustomerByFirstNameAsync(searchTerm);
			if (!customers.Any())
			{
				Console.WriteLine($"No customers found matching '{searchTerm}'.");
				return;
			}

			foreach (var customer in customers)
			{
				await DisplayCustomerDetails(customer);
				Console.WriteLine("---------------------------");
			}
		}

		public async Task AddNewCustomer()
		{
			Console.Clear();
			Console.WriteLine("========== Add New Customer ==========");

			try
			{
				var customer = new Customer();

				Console.Write("Enter First Name: ");
				customer.FirstName = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrWhiteSpace(customer.FirstName))
				{
					Console.WriteLine("First name is required.");
					return;
				}

				Console.Write("Enter Last Name: ");
				customer.LastName = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrWhiteSpace(customer.LastName))
				{
					Console.WriteLine("Last name is required.");
					return;
				}

				Console.Write("Enter Email (optional): ");
				customer.Email = Console.ReadLine();
				if (!string.IsNullOrWhiteSpace(customer.Email) && !IsValidEmail(customer.Email))
				{
					Console.WriteLine("Invalid email format.");
					return;
				}

				Console.Write("Enter Phone Number: ");
				customer.PhoneNumber = Console.ReadLine() ?? string.Empty;
				if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
				{
					Console.WriteLine("Phone number is required.");
					return;
				}

				var createdCustomer = await _customerService.CreateCustomerAsync(customer);
				Console.WriteLine($"Customer added successfully with ID: {createdCustomer.Id}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding customer: {ex.Message}");
			}
		}

		public async Task UpdateCustomer()
		{
			Console.Clear();
			await DisplayAllCustomers();
			Console.WriteLine("========== Update Customer ==========");
			Console.Write("Enter Customer ID to update: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
				return;
			}

			var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
			if (existingCustomer == null)
			{
				Console.WriteLine($"Customer with ID {id} not found.");
				return;
			}

			await DisplayCustomerDetails(existingCustomer);
			Console.WriteLine("\nEnter new details (press Enter to keep current values):");

			Console.Write($"First Name ({existingCustomer.FirstName}): ");
			string firstName = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(firstName))
			{
				existingCustomer.FirstName = firstName;
			}

			Console.Write($"Last Name ({existingCustomer.LastName}): ");
			string lastName = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(lastName))
			{
				existingCustomer.LastName = lastName;
			}

			Console.Write($"Email ({existingCustomer.Email ?? "N/A"}): ");
			string email = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(email))
			{
				if (IsValidEmail(email))
				{
					existingCustomer.Email = email;
				}
				else
				{
					Console.WriteLine("Invalid email format. Email will remain unchanged.");
				}
			}

			Console.Write($"Phone Number ({existingCustomer.PhoneNumber}): ");
			string phoneNumber = Console.ReadLine() ?? string.Empty;
			if (!string.IsNullOrWhiteSpace(phoneNumber))
			{
				existingCustomer.PhoneNumber = phoneNumber;
			}

			try
			{
				var updatedCustomer = await _customerService.UpdateCustomerAsync(existingCustomer);
				Console.WriteLine($"Customer with ID {updatedCustomer.Id} updated successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating customer: {ex.Message}");
			}
		}

		public async Task DeleteCustomer()
		{
			Console.Clear();
			await DisplayAllCustomers();
			Console.WriteLine("========== Delete Customer ==========");
			Console.Write("Enter Customer ID to delete: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format. Please enter a number.");
				return;
			}

			var customer = await _customerService.GetCustomerByIdAsync(id);
			if (customer == null)
			{
				Console.WriteLine($"Customer with ID {id} not found.");
				return;
			}

			await DisplayCustomerDetails(customer);
			Console.Write("\nAre you sure you want to delete this customer? (Y/N): ");
			string confirmation = Console.ReadLine() ?? string.Empty;

			if (confirmation.Trim().ToUpper().StartsWith("Y"))
			{
				bool result = await _customerService.DeleteCustomerAsync(id);
				if (result)
				{
					Console.WriteLine($"Customer with ID {id} deleted successfully.");
				}
				else
				{
					Console.WriteLine($"Failed to delete customer with ID {id}.");
				}
			}
			else
			{
				Console.WriteLine("Delete operation cancelled.");
			}
		}

		private bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}
	}
}
