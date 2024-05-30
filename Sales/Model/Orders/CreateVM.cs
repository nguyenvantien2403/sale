using Sale.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Model.Orders
{
	public class CreateVM
	{
		public string Status { get; set; }
		public Guid? UserId { get; set; }
		public string lastName { get; set; }
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
