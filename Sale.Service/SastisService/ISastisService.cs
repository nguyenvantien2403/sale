using Sale.Service.Dtos.OrdersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.SastisService
{
	public interface ISastisService
	{
		SastisDto GetAllData(int? year = null, int? months = null, int? weeeks = null, int? days = null);
	}
}
