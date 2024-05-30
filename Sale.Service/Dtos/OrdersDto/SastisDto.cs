using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.OrdersDto
{
	public class SastisDto
	{
        public float? orderSuccess { get; set; }
        public float? orderCancle { get; set; }
        public float? orderFailed { get; set; }
        public int? dataProduct { get; set; }

        public List<KeyValuePair<string, int>> listBranch  { get; set;}

        public int? dataCountBranchs { get; set; }

        public int? dataCountOrigins { get; set; }

        public double? totalByYear { get; set; }
        public double? totalByMonth { get; set; }

        public double? percenCreaseGP { get; set; }

    }
}
