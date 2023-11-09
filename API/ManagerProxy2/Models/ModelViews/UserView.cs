namespace ManagerProxy2.Models.ModelViews
{
	public class UserView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string? Role { get; set; }
		public string? PhotoFileName { get; set; }
		public int WalletId { get; set; }
		public double TotalMoney { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set;}
	}
}
