using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.ProductDto
{
	public class ProductDetailDto
	{



        public Guid Id { get; set; }
        public string? BranchName { get; set; }
		public string? CategoryId { get; set; }
		public string? OriginName { get; set; }
		[DisplayName("Tên sản phẩm")]
		public string ProductName { get; set; }
		public decimal? ProdcutPrice { get; set; }
		public int? ProductQuanlity { get; set; }
		public int? ProductNumber { get; set; }
		public string? ProductDescription { get; set; }
		public string? ProductOrigin { get; set; }
		public string? ProductMaterial { get; set; }
		public int? views { get; set; }
		public int? comment { get; set; }
		public int? rate { get; set; }
		public string? ProductType { get; set; }
		public int? ProductSold { get; set; }
		public DateTime? CreateAt { get; set; }
		public Guid? OriginId { get; set; }
		public Guid? BranchId { get; set; }
		public List<Sale.Service.Dtos.FileImageDto.FileImageDto> listFileAndImage { get; set; }
	}
}
