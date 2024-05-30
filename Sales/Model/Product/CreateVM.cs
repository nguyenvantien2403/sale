using System.ComponentModel;

namespace Sales.Model.Product
{
	public class CreateVM
	{
		public Guid? BranchId { get; set; }
		public string? CategoryId { get; set; }
		public Guid? OriginId { get; set; }
		public string ProductName { get; set; }
		public decimal? ProdcutPrice { get; set; }
		public int? ProductQuanlity { get; set; }
		public string? ProductDescription { get; set; }
		public string? ProductMaterial { get; set; }
		public int? views { get; set; }
		public int? comment { get; set; }
		public int? rate { get; set; }
		public string? ProductType { get; set; }
		public int? ProductSold { get; set; }
		public List<IFormFile>? ListFileImg { get; set; }
    }
}
