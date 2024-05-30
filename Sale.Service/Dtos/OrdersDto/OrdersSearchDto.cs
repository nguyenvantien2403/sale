using Sale.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.OrdersDto
{
	public class OrdersSearchDto : SearchBase
	{
        public Guid? UserId { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
