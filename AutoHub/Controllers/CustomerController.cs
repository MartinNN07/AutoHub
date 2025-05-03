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
    public class CustomerController : ICustomerController
    {
		private readonly ICustomerService _customerService;
		private readonly CustomerView _customerView;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
			_customerView = new CustomerView(customerService);
		}

		public async Task Run()
		{
			await _customerView.DisplayMenu();
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			return await _customerService.GetAllCustomersAsync();
		}

		public async Task<Customer> GetCustomerByIdAsync(int id)
		{
			return await _customerService.GetCustomerByIdAsync(id);
		}

		public async Task<IEnumerable<Customer>> SearchCustomersByFirstNameAsync(string searchTerm)
		{
			return await _customerService.GetCustomerByFirstNameAsync(searchTerm);
		}

		public async Task<Customer> CreateCustomerAsync(Customer customer)
		{
			// Validate incoming customer
			if (string.IsNullOrWhiteSpace(customer.FirstName))
			{
				throw new ArgumentException("First name is required.");
			}

			if (string.IsNullOrWhiteSpace(customer.LastName))
			{
				throw new ArgumentException("Last name is required.");
			}

			if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
			{
				throw new ArgumentException("Phone number is required.");
			}

			if (!string.IsNullOrWhiteSpace(customer.Email) && !IsValidEmail(customer.Email))
			{
				throw new ArgumentException("Email address is not valid.");
			}

			return await _customerService.CreateCustomerAsync(customer);
		}

		public async Task<Customer> UpdateCustomerAsync(Customer customer)
		{
			var existingCustomer = await _customerService.GetCustomerByIdAsync(customer.Id);

			// Validate incoming and existing customer
			if (existingCustomer == null)
			{
				throw new KeyNotFoundException($"Customer with ID {customer.Id} not found.");
			}

			if (string.IsNullOrWhiteSpace(customer.FirstName))
			{
				throw new ArgumentException("First name is required.");
			}

			if (string.IsNullOrWhiteSpace(customer.LastName))
			{
				throw new ArgumentException("Last name is required.");
			}

			if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
			{
				throw new ArgumentException("Phone number is required.");
			}

			if (!string.IsNullOrWhiteSpace(customer.Email) && !IsValidEmail(customer.Email))
			{
				throw new ArgumentException("Email address is not valid.");
			}

			return await _customerService.UpdateCustomerAsync(customer);
		}

		public async Task<bool> DeleteCustomerAsync(int id)
		{
			var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
			if (existingCustomer == null)
			{
				return false;
			}

			return await _customerService.DeleteCustomerAsync(id);
		}

		//email validation helper method
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
