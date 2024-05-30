using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.CommentDto;
using Sale.Service.Dtos.RateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.CommentsService
{
	public interface ICommentsService : IService<Comments>
	{
        List<CommentsDto> GetByProduct(Guid productId);
	}
}
