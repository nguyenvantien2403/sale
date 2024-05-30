using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos
{
	public class SMTP
	{
		public string SenderAddress { set; get; }
		public string SenderDisplayName { set; get; }
		public string UserName { set; get; }
		public string Password { set; get; }
		public string Host { set; get; }
		public int Port { set; get; }
		public bool EnableSSL { set; get; }
		public bool UseDefaultCredentials { set; get; }
		public bool IsBodyHTML { set; get; }
	}
}
