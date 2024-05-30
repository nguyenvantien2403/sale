using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.FileImageDto
{
	public class FileImageDto
	{
		public Guid? ProductId { get; set; }
		public string FilePath { get; set; }
		public string mime { get; set; }
		public string extension { get; set; }
		public DateTime? CreateAt { get; set; }
		public double? fileSize { get; set; }
		public string? FileName { get; set; }
	}
}
