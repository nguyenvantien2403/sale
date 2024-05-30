using Sale.Domain.Entities;
using Sale.Repository.CommentsRepository;
using Sale.Repository.Core;
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
	public class CommentsService : Service<Comments>, ICommentsService
	{
		private readonly ICommentsRepository _commentsRepository;
		public CommentsService(IRepository<Comments> repository, ICommentsRepository commentsRepository) : base(commentsRepository)
		{
			_commentsRepository = commentsRepository;
		}

		public List<CommentsDto> GetByProduct(Guid productId)
		{
			try
			{
				var query = from q in _commentsRepository.GetQueryable()
							where q.ProductId == productId
                            select new CommentsDto
							{
								ProductId = q.ProductId,
								userPost = q.userPost,
								comment = q.comment,
								email = q.email,
								createAt = q.CreatedDate
							};
				return query.ToList();
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
