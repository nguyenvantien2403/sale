using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.RateDto
{
	public class RateDto
	{

		public Guid? UserId { get; set; }
		public Guid? ProductId { get; set; }
		public int RateQuanlity { get; set; }
		public DateTime CreateAt { get; set; }

	}
}
