using Sale.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain.Entities
{
	[Table("Products")]
	public class Product : AuditableEntity
	{
		public Guid? BranchId { get; set; }
		public string? CategoryId { get; set;}
		public Guid? OriginId { get; set; }
		public string ProductName { get; set; }
        public decimal? ProdcutPrice { get; set; }
        public int? ProductQuanlity { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductMaterial { get; set; }
        public int?  views { get; set; }
        public int? comment { get; set; }
        public int? rate { get; set; }
		public string? ProductType { get; set; }
        public int?  ProductSold { get; set; }

    }
}
