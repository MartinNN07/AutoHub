using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.Data.Models;

namespace AutoHub.Business.Services
{
	public interface IBrandService
	{
		Task<Brand> CreateCustomerAsync(Brand brand);

		Task<IEnumerable<Brand>> GetAllCustomersAsync();
		Task<Brand> GetCustomerByIdAsync(int id);
		Task<IEnumerable<Brand>> GetCustomersByNameAsync(string searchTerm);

		Task<Brand> UpdateCustomerAsync(Brand brand);

		Task<bool> DeleteCustomerAsync(int id);
	}
}
