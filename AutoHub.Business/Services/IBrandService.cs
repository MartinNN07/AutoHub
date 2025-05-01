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
		Task<Brand> CreateBrandAsync(Brand brand);

		Task<IEnumerable<Brand>> GetAllBrandsAsync();
		Task<Brand> GetBrandByIdAsync(int id);
		Task<IEnumerable<Brand>> GetBrandsByNameAsync(string searchTerm);

		Task<Brand> UpdateBrandAsync(Brand brand);

		Task<bool> DeleteBrandAsync(int id);
	}
}
