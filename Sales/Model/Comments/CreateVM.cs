namespace Sales.Model.Comments
{
	public class CreateVM
	{
        public Guid? ProductId { get; set; }
        public string? userPost { get; set; }
        public string? email { get; set; }
        public string? comment { get; set; }
    }
}
