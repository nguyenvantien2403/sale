using Sale.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain.Entities
{
	[Table("Files")]
	public class FileImage : AuditableEntity
	{
		public Guid? ProductId { get; set; }
        public string FilePath { get; set; }
        public string mime { get; set; }
        public string extension { get; set; }
		public Product product { get; set; }
		public double fileSize { get; set; }
		public string FileName { get; set; }
    }
}
