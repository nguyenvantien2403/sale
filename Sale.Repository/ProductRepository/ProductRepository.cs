using Sale.Domain;
using Sale.Domain.Entities;
using Sale.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Repository.ProductRepository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		public ProductRepository(SaleContext options) : base(options)
		{
			
		}
	}
}
