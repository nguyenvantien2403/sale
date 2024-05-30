namespace Sales.Model.Rate
{
	public class CreateVM
	{

		public Guid? UserId { get; set; }
		public Guid? ProductId { get; set; }
		public int RateQuanlity { get; set; }
	}
}
