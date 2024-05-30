using Sale.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Dtos.CommentDto
{
	public class CommentsSearchDto : SearchBase
	{
		public Guid? UserId { get; set; }
		public Guid? ProductId { get; set; }

	}
}
