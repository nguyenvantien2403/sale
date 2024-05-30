using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos
{
	public class ResetPassword
	{
        public string username { get; set; }
        [Required, DataType(DataType.Password)]
		public string Password { get; set; }
		[Required, DataType(DataType.Password)]
		public string newPassword { get; set; }

	}
}
