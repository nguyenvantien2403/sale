namespace Sales.Model.AppRole
{
	public class CreateVM
	{
        public string Name { get; set; }
        public string? NormalizedName { get; set; }
        public int? ConcurrencyStamp { get ; set; }

	}
}
