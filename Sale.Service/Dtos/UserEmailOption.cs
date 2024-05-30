using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos
{
	public class UserEmailOption
	{
		public string toEmails { set; get; }
		public string subject { set; get; }
		public string body { set; get; }
		public List<KeyValuePair<string, string>> keyValuePairs { set; get; }
	}
}
