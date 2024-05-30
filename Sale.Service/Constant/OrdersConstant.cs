using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Constant
{
	public class OrdersConstant
	{
		[DisplayName("Đang chuẩn bị")]
		public static string DANGCHUANBI { get; } = "DANGCHUANBI";
		[DisplayName("Chờ xác nhận")]

		public static string CHOXACNHAN { get; } = "CHOXACNHAN";
		[DisplayName("Đang giao")]

		public static string DANGIAO { get; } = "DANGIAO";
		[DisplayName("Đã nhận")] 
		public static string DANHAN { get; } = "DANHAN";
		[DisplayName("Đã gửi")]
		public static string DAGUI { get; } = "DAGUI";
		[DisplayName("Đã thanh toán")] 
		public static string DATHANHTOAN { get; } = "DATHANHTOAN";
		[DisplayName("Thành công")] 
		public static string THANHCONG { get; } = "THANHCONG";
		[DisplayName("Đã hủy")]
		public static string DAHUY { get; } = "DAHUY";
		
		[DisplayName("Thất bại")] 
		public static string THATBAI { get; } = "THATBAI";


	}
}
