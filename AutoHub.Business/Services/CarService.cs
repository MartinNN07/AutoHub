﻿using AutoHub.Business.Services.Interfaces;
using AutoHub.Data.Database;
using AutoHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Business.Services
{
	public class CarService : ICarService
	{
		private readonly AutoHubDbContext _context;

		public CarService(AutoHubDbContext context)
		{
			_context = context;
		}
		public async Task<Car> CreateCarAsync(Car car)
		{
            // Validate the car object
            if (car == null)
				throw new ArgumentNullException(nameof(car));

			await _context.Cars.AddAsync(car);
			await _context.SaveChangesAsync();

			return car;
		}

		public async Task<bool> DeleteCarAsync(int id)
		{
            // Validate the ID
            var car = await _context.Cars.FindAsync(id);

			if (car == null)
				return false;

			_context.Cars.Remove(car);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<Car>> GetAllCarsAsync()
		{
            // Fetch all cars from the database
            var list = await _context.Cars.ToListAsync();
			return list;
		}

		public async Task<Car> GetCarByIdAsync(int id)
		{
            // Validate the ID
            return await _context.Cars
				.Include(c => c.Brand)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<IEnumerable<Car>> GetCarsByModelAsync(string searchTerm)
		{
            // Validate the search term
            if (string.IsNullOrWhiteSpace(searchTerm))
				return await GetAllCarsAsync();

			return await _context.Cars
				.Where(c => c.Model.ToLower().Contains(searchTerm.ToLower()))
				.ToListAsync();
		}

		public async Task<Car> UpdateCarAsync(Car car)
		{
            //	Validate the car object
            if (car == null)
				throw new ArgumentNullException(nameof(car));

			var existingCar = await _context.Cars.FindAsync(car.Id);

			if (existingCar == null)
				throw new KeyNotFoundException($"Brand with ID {car.Id} not found");

			_context.Entry(existingCar).CurrentValues.SetValues(car);

			await _context.SaveChangesAsync();

			return existingCar;
		}
	}
}
