using Sale.Domain.Entities;
using Sale.Repository.RateRepostiory;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.RateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.RateService
{
	public class RateService : Service<Rate> , IRateService
	{

		private readonly IRateRepository _rateRepostiory;
		public RateService(IRateRepository rateRepostiory) : base(rateRepostiory)
		{
			_rateRepostiory = rateRepostiory;
		}

		public PageList<RateDto> GetByProduct(Guid prroductId, RateSearchDto searchDto)
		{
			try
			{
				var query = from q in _rateRepostiory.GetQueryable()
							where q.ProductId == prroductId
							select new RateDto
							{
								ProductId = q.ProductId,
								UserId = q.UserId,
								CreateAt = q.CreatedDate,
								RateQuanlity = q.RateQuanlity,
							};
				if (searchDto.PageSize == null && searchDto.PageSize <= 0)
				{
					searchDto.PageSize = 10;
				}
				if (searchDto.PageIndex == null && searchDto.PageIndex <= 0)
				{
					searchDto.PageIndex = 1;
				}
				var data = PageList<RateDto>.Cretae(query,searchDto);
				return data;
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
