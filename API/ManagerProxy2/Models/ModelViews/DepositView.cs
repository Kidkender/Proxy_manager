namespace ManagerProxy2.Models.ModelViews
{
	public class DepositView
	{
		public int Id { get; set; }
		public int WalletId { get; set; }
		public double Money { get; set; }
		public double TotalMoney { get; set; }
		public string? Email { get; set; }
		public int UserId { get; set; }
		public string? Username { get; set; }
		public string? Name { get; set; }
		public DateTime Created { get; set; }

	}
}
