using Sale.Domain.Entities;
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
	public interface IRateService : IService<Rate>
	{
		PageList<RateDto> GetByProduct(Guid prroductId, RateSearchDto searchDto);
	}
}
