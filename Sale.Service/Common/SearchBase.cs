using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Common
{
	public class SearchBase
	{
        public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public string? fieldName { get; set; }
		public string? typeOrder { get; set; }
	}
}
