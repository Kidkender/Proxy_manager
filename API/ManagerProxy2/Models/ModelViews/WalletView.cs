namespace ManagerProxy.Models.ModelViews
{
	public class WalletView 
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public double TotalMoney { get; set; }
	}
}
