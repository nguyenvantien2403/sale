using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.BranchDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.BranchService
{
	public interface IBranchService : IService<Branch>
	{
		Task<PageList<BranchDto>> GetDataByPage(BranchSearchDto searchDto);
		Branch? FindById(Guid id);
		Task<List<BranchDto>> getAll(BranchSearchDto searchDto);
	}
}
