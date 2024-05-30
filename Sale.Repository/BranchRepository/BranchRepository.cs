using Sale.Domain;
using Sale.Domain.Entities;
using Sale.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Repository.BranchRepository
{
	public class BranchRepository : Repository<Branch>, IBranchRepository
	{
		public BranchRepository(SaleContext context) : base(context)
		{

		}
	}
}
