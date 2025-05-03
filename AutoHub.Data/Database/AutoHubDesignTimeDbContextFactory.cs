using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Database
{
	public class AutoHubDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AutoHubDbContext>
	{
		public AutoHubDbContext CreateDbContext(string[] args)
		{
			//construct a new DbContextOptionsBuilder using the connection string
			var options = new DbContextOptionsBuilder<AutoHubDbContext>()
				.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AutoHubDb;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new AutoHubDbContext(options.Options);
		}
	}
}
