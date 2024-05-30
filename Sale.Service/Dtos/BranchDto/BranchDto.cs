using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.BranchDto
{
	public class BranchDto
	{
		public Guid id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? BranchName { get; set; }


        public int CountProduct { get; set; }

    }
}
