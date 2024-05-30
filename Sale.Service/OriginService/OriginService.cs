using Microsoft.AspNetCore.Mvc.Filters;
using Sale.Domain.Entities;
using Sale.Repository.OriginRepository;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.OriginDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.OriginService
{
	public class OriginService : Service<Origin>, IOriginService
	{
		private readonly IOriginRepository _originRepository;
		public OriginService(
			IOriginRepository originRepository) : base(originRepository)
		{
			_originRepository = originRepository; 
		}

		public List<OriginDto>? GetAll(OriginSearchDto searchDto)
		{
			var querry = from q in _originRepository.GetQueryable()
						 select new OriginDto
						 {
							 Id = q.Id,
							 OriginName = q.OriginName,
							 
						 };

			if (searchDto != null)
			{
				if (searchDto.OriginName != null)
				{
					string normalStr = searchDto.OriginName.RemoveAccentsUnicode().ToLower();
					querry = querry.Where(delegate (OriginDto x)
					{
						return x.OriginName.ToLower().RemoveAccentsUnicode().Contains(normalStr);
					}).AsQueryable();
				}
			}


			return querry.ToList();
		}
	
		public async Task<PageList<OriginDto?>> GetDataBypage(OriginSearchDto searchDto)
		{
			try
			{
				var querry = from q in _originRepository.GetQueryable()
							 select new OriginDto
							 {
								 OriginName = q.OriginName
							 };

				if(searchDto != null)
				{
					if (searchDto.OriginName !=	null) 
					{
						string normalStr = searchDto.OriginName.RemoveAccentsUnicode().ToLower();
						querry = querry.Where(delegate (OriginDto x)
						{
							return x.OriginName.ToLower().RemoveAccentsUnicode().Contains(normalStr);
						}).AsQueryable();
					}
				}
				if(searchDto.PageIndex == null || searchDto.PageIndex <= 0)
				{
					searchDto.PageIndex = 1;
				}
				if(searchDto.PageSize == null || searchDto.PageSize <= 0)
				{
					searchDto.PageSize = 10;
				}

				var data = PageList<OriginDto>.Cretae(querry, searchDto);
				return data;
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
