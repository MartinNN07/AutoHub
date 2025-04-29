using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Models
{
	internal class Brand
	{
		int Id { get; set; }
		string Name { get; set; }
		string CountryOfOrigin { get; set; }
		ICollection<Car> Cars { get; set; }
	}
}
