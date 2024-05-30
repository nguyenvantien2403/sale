using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.UserDto
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Tài khoản không được để trống")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Địa chỉ Email không được để trống")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Họ Tên không được để trống")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Số điện thoại không được để trống")]
		public string PhoneNumber { get; set; }

		public string? RoleName { get; set; }
	}
}
