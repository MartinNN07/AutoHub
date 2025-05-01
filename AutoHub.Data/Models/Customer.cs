using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.Data.Models
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[MaxLength(100)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MaxLength(20)]
		public string PhoneNumber { get; set; }

		public virtual ICollection<Sale> Sales { get; set; }
	}
}
