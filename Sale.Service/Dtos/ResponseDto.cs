using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos
{
	public class ResponseWithMessageDto
	{
        public string Message { get; set; }
        public string Status { get; set; }
        public int Code { get; set; }
    }

	public class ResponseWithDataDto<T>
	{
		public string? Message { get; set; }
		public string? Status { get; set; }
		public int Code { get; set; }
		public T? Data { get; set; }
	}


}
