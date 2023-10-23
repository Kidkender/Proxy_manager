using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerProxy2.Models.Entity
{
	[Table("WalletHistoryTransaction")]
	public class WalletHistoryTransaction
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int UserFromId { get; set; }
		[Required]
		public int UserToId { get; set; }
		[Required]
		public double Money { get; set; }
		public string? Note { get; set; }

	}
}
