using Sale.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Model.Orders
{
	public class EditVM
	{
		public string Status { get; set; }
		public Guid? UserId { get; set; }
		public double TotalCount { get; set; }
		public double Shipping { get; set; }
		public DateTime? ShippingDate { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		[ForeignKey("CartId")]
		public virtual List<Cart> Carts { get; set; }
	}
}
	