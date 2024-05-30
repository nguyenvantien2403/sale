using Sale.Domain;
using Sale.Domain.Entities;
using Sale.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Repository.RateRepostiory
{
	public class RateRepository : Repository<Rate>, IRateRepository
	{
		public RateRepository(SaleContext context) : base(context)
		{
		}
	}
}
