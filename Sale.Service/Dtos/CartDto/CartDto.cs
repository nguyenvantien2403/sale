using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.CartDto
{
	public class CartDto
	{
		public Guid? Id { get; set; }
		public int? count { get; set; }
        public Guid? produtId { get; set; }
        public string? ProductName { get; set; }
		public decimal? Price { get; set; }
    }
}
