using Sale.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.FileImageDto
{
	public class FileImageSearchDto : SearchBase
	{
		public Guid? ProductId { get; set; }
    }
}
