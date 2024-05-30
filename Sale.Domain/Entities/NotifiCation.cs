using Sale.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain.Entities
{
	public class NotifiCation : AuditableEntity
	{
		public Guid? SendUserId { get; set; }
		public string Title { get; set; }
		public string  Content { get; set; }

	}
}
