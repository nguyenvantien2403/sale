using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.ProductService
{
	public interface IProductService : IService<Product>
	{
		Task<PageList<ProductDto>> GetDataByPage(ProductSearchDto searchDto);
		ProductDetailDto? FindDetailProduct(Guid id);
		List<ProductDto>? GetBestSaleProduct();
	}
}
