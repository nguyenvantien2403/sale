using Sale.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain.Entities
{
	public class Orders : AuditableEntity
	{
        public string Status { get; set; }
        public string lastName { get; set; }

        public Guid? UserId { get; set; }
        public string firstName { get; set; }
        public double totalPrice { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string orderNotes { get; set; }
        [ForeignKey("CartId")]
        public virtual List<Cart> Carts { get; set; }

    }
}
