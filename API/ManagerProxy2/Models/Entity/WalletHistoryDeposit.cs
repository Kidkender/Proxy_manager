using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerProxy2.Models.Entity
{
	[Table("WalletHistoryDeposit")]
	public class WalletHistoryDeposit
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int WalletId { get; set; }
		[Required]
		public double Money { get; set; }
		public string? Note { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

	}
}
