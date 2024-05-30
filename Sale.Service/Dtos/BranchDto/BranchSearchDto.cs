using Sale.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.BranchDto
{
	public class BranchSearchDto : SearchBase
	{
		public string? BranchName { get; set; }
	}
}
