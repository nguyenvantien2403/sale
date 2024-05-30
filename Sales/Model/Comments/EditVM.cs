namespace Sales.Model.Comments
{
	public class EditVM
	{
		public Guid? UserId { get; set; }
		public Guid? ProductId { get; set; }
		public string Comment { get; set; }
	}
}
