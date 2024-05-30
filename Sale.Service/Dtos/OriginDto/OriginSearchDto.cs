using Sale.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.OriginDto
{
	public class OriginSearchDto : SearchBase
	{
		public string? OriginName { get; set; }
	}
}
