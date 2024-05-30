using Sale.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain.Entities
{
	[Table("Origins")]
	public class Origin : AuditableEntity
	{
        public string? OriginName { get; set; }
    }
}
