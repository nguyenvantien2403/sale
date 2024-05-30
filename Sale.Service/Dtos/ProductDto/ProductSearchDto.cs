using Sale.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.ProductDto
{
	public class ProductSearchDto : SearchBase	
	{
		public string? ProductName { get; set; }

		public List<Dictionary<string,decimal>>? listPrice { get; set; }
		
        public Guid? BranchId { get; set; }

        public Guid? OriginId  { get; set; }

		public string? SortBy { get; set; }

    }
}
