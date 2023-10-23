using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerProxy2.Models.Entity
{
	[Table("Wallet")]
	public class Wallet
	{
		[Key] public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public double TotalMoney { get; set; } = 0;
	}
}
