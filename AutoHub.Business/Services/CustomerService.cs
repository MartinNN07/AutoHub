using AutoHub.Data.Database;
using AutoHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Business.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly AutoHubDbContext _context;
		public CustomerService(AutoHubDbContext context)
		{
			_context = context;
		}
		public async Task<Customer> CreateCustomerAsync(Customer customer)
		{
			if (customer == null)
				throw new ArgumentNullException(nameof(customer));

			await _context.Customers.AddAsync(customer);
			await _context.SaveChangesAsync();

			return customer;
		}

		public async Task<bool> DeleteCustomerAsync(int id)
		{
			var customer = await _context.Customers.FindAsync(id);

			if (customer == null)
				return false;

			_context.Customers.Remove(customer);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			return await _context.Customers.ToListAsync();
		}

		public async Task<IEnumerable<Customer>> GetCustomerByFirstNameAsync(string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return await GetAllCustomersAsync();

			return await _context.Customers
				.Where(c => c.FirstName.Contains(searchTerm))
				.ToListAsync();
		}

		public async Task<Customer> GetCustomerByIdAsync(int id)
		{
			return await _context.Customers
				.Include(c => c.Sales)
				.ThenInclude(s => s.Car)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Customer> UpdateCustomerAsync(Customer customer)
		{
			if (customer == null)
				throw new ArgumentNullException(nameof(customer));

			var existingCustomer = await _context.Customers.FindAsync(customer.Id);
			if (existingCustomer == null)
				throw new KeyNotFoundException($"Customer with ID {customer.Id} not found");

			_context.Entry(existingCustomer).CurrentValues.SetValues(customer);
			await _context.SaveChangesAsync();
			return existingCustomer;
		}
	}
}
