using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Models
{
	public class Brand
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(50)]
		public string? CountryOfOrigin { get; set; }

		public ICollection<Car>? Cars { get; set; }
	}
}
