using Sale.Domain.Entities;
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
	public interface IOriginService : IService<Origin>
	{
		Task<PageList<OriginDto>> GetDataBypage(OriginSearchDto searchDto);

		List<OriginDto>? GetAll(OriginSearchDto? searchDto);

	}
}
