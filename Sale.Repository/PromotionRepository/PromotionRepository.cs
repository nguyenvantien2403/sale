using Sale.Domain;
using Sale.Domain.Entities;
using Sale.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Repository.PromotionRepository
{
	public class PromotionRepository : Repository<Promotion> , IPromotionRepository
	{
		public PromotionRepository(SaleContext context) : base(context)
		{
		}
	}
}
