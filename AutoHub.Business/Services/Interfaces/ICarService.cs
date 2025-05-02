using AutoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Business.Services.Interfaces
{
	public interface ICarService
	{
		Task<Car> CreateCarAsync(Car car);

		Task<IEnumerable<Car>> GetAllCarsAsync();
		Task<Car> GetCarByIdAsync(int id);
		Task<IEnumerable<Car>> GetCarsByModelAsync(string searchTerm);

		Task<Car> UpdateCarAsync(Car car);

		Task<bool> DeleteCarAsync(int id);
	}
}
