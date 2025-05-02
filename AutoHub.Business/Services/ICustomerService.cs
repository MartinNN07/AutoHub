using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Business.Services
{
	public interface ICustomerService
	{
		Task<Customer> CreateCustomerAsync(Customer customer);

		Task<IEnumerable<Customer>> GetAllCustomersAsync();
		Task<Customer> GetCustomerByIdAsync(int id);
		Task<IEnumerable<Customer>> GetCustomerByFirstNameAsync(string searchTerm);

		Task<Customer> UpdateCustomerAsync(Customer customer);

		Task<bool> DeleteCustomerAsync(int id);
	}
}
