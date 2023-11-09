namespace ManagerProxy2.Models.ModelViews
{
	public class TransactionView
	{
		public int Id { get; set; }
		public double Money { get; set; }
		public int UserFromId { get; set; }
		public int UserToId { get; set; }
		public string? UsernameFrom { get; set; }
		public string? UsernameTo { get; set; }
		public string? NameFrom { get; set; }
		public string? NameTo { get; set; }
		public string? EmailFrom { get; set; }
		public string? EmailTo { get; set; }
		public DateTime Created { get; set; }

	}
}
