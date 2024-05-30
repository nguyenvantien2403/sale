using Sale.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain.Entities
{
	public class Promotion : AuditableEntity
	{
		public string PromotionName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double Percent { get; set; }
        public bool isPublic { get; set; }
    }
}
