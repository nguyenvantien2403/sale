using Microsoft.AspNetCore.Http.HttpResults;
using Sale.Domain.Entities;
using Sale.Repository.OrdersRepository;
using Sale.Service.Common;
using Sale.Service.Constant;
using Sale.Service.Core;
using Sale.Service.Dtos.FileImageDto;
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
	public class OrdersService : Service<Orders> , IOrdersService
	{
		private readonly IOrdersRepository _ordersRepository;
		public OrdersService(IOrdersRepository ordersRepository) : base(ordersRepository)
		{
			_ordersRepository = ordersRepository;
		}
		public OriginDto? FindDetailProduct(Guid id)
		{
			try
			{
				var query = (from q in _ordersRepository.GetQueryable()
							
							 where q.Id == id && (q.IsDelete == null || q.IsDelete == false)
							 select new OriginDto
							 {
								
							 }).FirstOrDefault();

				return query;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<PageList<OrdersDto>> GetDataByPage(OrdersSearchDto searchDto)
		{
			try
			{
				var query = from q in _ordersRepository.GetQueryable()
							
							where q.IsDelete == false || q.IsDelete == null
							select new OrdersDto
							{
								Id = q.Id,
								ShippingDate = q.ShippingDate,
								Createat = q.CreatedDate,
								address = q.address,
								Status = ConstantExtension.GetDisPlayConstant<OrdersConstant>(q.Status),
							    mobile = q.mobile,
								totalPrice = q.totalPrice,
								firstName = q.firstName,
								lastName = q.lastName,
								email = q.email,
								orderNotes = q.orderNotes,
								Carts = q.Carts
							};

				if (searchDto != null)
				{
					if (searchDto.UserId != null)
					{
						query = query.Where(x => x.UserId == searchDto.UserId);
					}
					if (searchDto.PhoneNumber != null)
					{
						query = query.Where(x => x.mobile == searchDto.PhoneNumber);
					}
					if (searchDto.PageIndex == null || searchDto.PageIndex <= 0)
					{
						searchDto.PageIndex = 1;
					}
					if (searchDto.PageSize == null || searchDto.PageSize <= 0)
					{
						searchDto.PageSize = 1;
					}
				}
				var items = PageList<OrdersDto>.Cretae(query, searchDto);
				return items;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
