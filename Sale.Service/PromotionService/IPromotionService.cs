using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.BranchDto;
using Sale.Service.Dtos.PromotionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.PromotionService
{
	public interface IPromotionService : IService<Promotion>
	{
		Task<PageList<PromotionDto>> GetDataByPage(PromotionSearchDto searchDto);
	}
}
