using Microsoft.EntityFrameworkCore;
using Sale.Domain;
using Sale.Repository.BranchRepository;
using Sale.Repository.OrdersRepository;
using Sale.Repository.OriginRepository;
using Sale.Repository.ProductRepository;
using Sale.Service.Constant;
using Sale.Service.Dtos.OrdersDto;
using Sale.Service.OrdersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.SastisService
{
	public class SastisService : ISastisService
	{

		private readonly SaleContext context;
		private readonly IOrdersService _service;
		private readonly IProductRepository _productRepository;
		private readonly IBranchRepository _branchRepository;
		private readonly IOrdersRepository _ordersRepository;
		private readonly IOriginRepository _originRepository;
		public SastisService(SaleContext saleContext)
		{
			context = saleContext;
		}

		public SastisDto GetAllData(int? year = null,int? months = null, int? weeeks = null,int? days = null)
		{
			var sastis = new SastisDto();

			var countod = context.Orders.Count();
			var querrycountProducts = context.Products.AsQueryable();
			sastis.dataCountBranchs = context.Branchs.Count();
			sastis.dataCountOrigins = context.Origin.Count();
			sastis.dataProduct = context.Products.Count();
			
			sastis.orderSuccess = (float)(context.Orders.Where(x => x.Status == OrdersConstant.THANHCONG && x.CreatedDate.Year == DateTime.Now.Year).Count()) / countod;
			sastis.orderCancle = (float)(context.Orders.Where(x => x.Status == OrdersConstant.DAHUY && x.CreatedDate.Year == DateTime.Now.Year).Count()) / countod;
			sastis.orderFailed = (float)(context.Orders.Where(x => x.Status == OrdersConstant.THATBAI && x.CreatedDate.Year == DateTime.Now.Year).Count()) / countod;
			sastis.listBranch = context.Branchs.AsQueryable().Select(x => new KeyValuePair<string, int>(x.BranchName,context.Products.Where(p => p.BranchId == x.Id).Count())).ToList();
			sastis.totalByYear = context.Orders.Where(t => t.Status == OrdersConstant.THANHCONG && t.CreatedDate.Year == DateTime.Now.Year).Sum(t => t.totalPrice);

			sastis.totalByMonth = context.Orders.Where(t => t.Status == OrdersConstant.THANHCONG && t.CreatedDate.Year == DateTime.Now.Year && t.CreatedDate.Month == DateTime.Now.Month).Sum(t => t.totalPrice);
			if (context.Orders.Where(t => t.Status == OrdersConstant.THANHCONG && t.CreatedDate.Year == DateTime.Now.Year - 1).Sum(t => t.totalPrice) > 0)
			{
				sastis.percenCreaseGP = (sastis.totalByYear / context.Orders.Where(t => t.Status == OrdersConstant.THANHCONG && t.CreatedDate.Year == DateTime.Now.Year - 1).Sum(t => t.totalPrice)) * 100;
			}else
			{
				sastis.percenCreaseGP = 100;
			}
			var dataCountOrders = context.Orders.AsQueryable();
			if (year != null && year > 0)
			{
				sastis.dataProduct = querrycountProducts.Where(x => x.CreatedDate.Year == year).Count();
				sastis.orderSuccess = (float) (dataCountOrders.Where(x => x.Status == OrdersConstant.THANHCONG && x.CreatedDate.Year == year).Count()) / countod;
				sastis.orderFailed = (float) (dataCountOrders.Where(x => x.Status == OrdersConstant.THATBAI && x.CreatedDate.Year == year).Count()) / countod;
				sastis.orderCancle = (float) (dataCountOrders.Where(x => x.Status == OrdersConstant.DAHUY && x.CreatedDate.Year == year).Count()) / countod;


				sastis.totalByYear = context.Orders.Where(t => t.Status == OrdersConstant.THANHCONG && t.CreatedDate.Year == year).Sum(t => t.totalPrice);
				sastis.totalByMonth = context.Orders.Where(t => t.Status == OrdersConstant.THANHCONG && t.CreatedDate.Year == year && t.CreatedDate.Month == months).Sum(t => t.totalPrice);


			}
			else if(months != null && months > 0)
			{
				sastis.dataProduct = querrycountProducts.Where(x => x.CreatedDate.Month == months && x.CreatedDate.Year == year).Count();
			}
            return sastis;
			
		}

	}
}
