using Sale.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.PromotionDto
{
	public class PromotionSearchDto : SearchBase
	{
		public string? PromotionName { get; set; }
		public DateTime? SearchTime { get; set; }
	
	}
}
