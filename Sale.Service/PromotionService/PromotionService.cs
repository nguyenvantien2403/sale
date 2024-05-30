using Microsoft.AspNetCore.Http.HttpResults;
using Sale.Domain.Entities;
using Sale.Repository.Core;
using Sale.Repository.PromotionRepository;
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
	public class PromotionService : Service<Promotion>, IPromotionService
	{
		private readonly IPromotionRepository _promotionRepository;
		public PromotionService(IPromotionRepository promotionRepository) : base(promotionRepository)
		{
			_promotionRepository = promotionRepository;
		}

		//public Promotion? FindById(Guid id)
		//{
		//	try
		//	{
		//		var branch = from q in _promotionRepository.GetQueryable()
		//					 where q.Id == id && (q.IsDelete == false || q.IsDelete == null)
		//		return branch.FirstOrDefault();
		//	}
		//	catch (Exception e)
		//	{
		//		return null;
		//	}
		//}

		public async Task<PageList<PromotionDto>> GetDataByPage(PromotionSearchDto searchDto)
		{
			try
			{
				var query = from q in _promotionRepository.GetQueryable()
							where q.IsDelete == null || q.IsDelete == false
							select new PromotionDto
							{
								Percent = q.Percent,
								id = q.Id,
								StartTime = q.StartTime,
								EndTime = q.EndTime,
								isPublic = q.isPublic,
								PromotionName = q.PromotionName,
								CreateAt = q.CreatedDate,
							};

				if (searchDto != null)
				{
					if (!string.IsNullOrEmpty(searchDto.PromotionName))
					{
						query = query.Where(delegate (PromotionDto b)
						{
							return b.PromotionName.RemoveAccentsUnicode().ToLower().Contains(searchDto.PromotionName.RemoveAccentsUnicode().ToLower());
						}).AsQueryable();
					}
					if (searchDto.SearchTime != null)
					{
						query = query.Where(x => x.StartTime <= searchDto.SearchTime && x.EndTime >= searchDto.SearchTime);
					}
					if (searchDto.PageIndex == null || searchDto.PageIndex <= 0)
					{
						searchDto.PageIndex = 1;
					}
					if (searchDto.PageSize == null || searchDto.PageSize <= 0)
					{
						searchDto.PageSize = 10;
					}
				}
				else
				{
					query = query.OrderByDescending(a => a.StartTime);
				}
				var items = PageList<PromotionDto>.Cretae(query, searchDto);
				return items;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
