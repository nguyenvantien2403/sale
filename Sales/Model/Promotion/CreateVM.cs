namespace Sales.Model.Promotion
{
	public class CreateVM
	{
		public string PromotionName { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public double Percent { get; set; }
		public bool isPublic { get; set; }
	}
}
