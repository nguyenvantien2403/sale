using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.OrdersDto;
using Sale.Service.Dtos.OriginDto;
using Sale.Service.Dtos.ProductDto;
using Sales.Model.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.OrdersService
{
	public interface IOrdersService : IService<Orders>
	{
		Task<PageList<OrdersDto>> GetDataByPage(OrdersSearchDto searchDto);
		OriginDto? FindDetailProduct(Guid id);
	}
}
