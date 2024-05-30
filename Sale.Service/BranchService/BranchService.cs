using Sale.Domain.Entities;
using Sale.Repository.BranchRepository;
using Sale.Repository.Core;
using Sale.Repository.ProductRepository;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.BranchDto;
using Sale.Service.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.BranchService
{
	public class BranchService : Service<Branch>, IBranchService
	{


		private readonly IBranchRepository _branchRepository;
		private readonly IProductRepository _productRepository;
        public BranchService(IRepository<Branch> repository, IBranchRepository branchRepository, IProductRepository productRepository) : base(repository)
		{
			_productRepository = productRepository;
			_branchRepository = branchRepository;
		}

		public Branch? FindById(Guid id)
		{
			try
			{
				var branch = from q in _branchRepository.GetQueryable()
							 where q.Id == id && (q.IsDelete == false || q.IsDelete == null)
							 select new Branch
							 {
								 BranchName = q.BranchName,
								 CreatedBy = q.CreatedBy,
								 CreatedDate = q.CreatedDate,
								 IsDelete = q.IsDelete,
								 CreatedID = q.CreatedID,
								 UpdatedDate = q.UpdatedDate,
								 Id = q.Id,
								 DeleteTime = q.DeleteTime,
								 UpdatedBy = q.UpdatedBy,
							 };
				return branch.FirstOrDefault();
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public async Task<PageList<BranchDto>> GetDataByPage(BranchSearchDto searchDto)
		{
			try
			{
				var query =	from q in _branchRepository.GetQueryable()

							join protbl in _productRepository.GetQueryable() on q.Id equals protbl.BranchId into pro
							where q.IsDelete == null || q.IsDelete == false
							select new BranchDto
							{
								BranchName = q.BranchName,
								id = q.Id,
								CountProduct = pro.Count(),
								CreateDate =  q.CreatedDate
							};

				if (searchDto != null)
				{
					if (!string.IsNullOrEmpty(searchDto.BranchName))
					{
						query = query.Where(delegate (BranchDto b)
						{
							return b.BranchName.RemoveAccentsUnicode().ToLower().Contains(searchDto.BranchName.RemoveAccentsUnicode().ToLower());
						}).AsQueryable();
					}
					if(searchDto.PageIndex == null || searchDto.PageIndex <= 0)
					{
						searchDto.PageIndex = 1;
					}
					if (searchDto.PageSize == null || searchDto.PageSize <= 0)
					{
						searchDto.PageSize = 10;
					}
				} else
				{
					query = query.OrderByDescending(a => a.BranchName);
				}
				var items = PageList<BranchDto>.Cretae(query, searchDto);
				return items;
			}
			catch (Exception ex)
			{
				return null;
			}
		}


		public async Task<List<BranchDto>> getAll(BranchSearchDto searchDto)
		{
			try
			{
				var query = from q in _branchRepository.GetQueryable()

							join protbl in _productRepository.GetQueryable() on q.Id equals protbl.BranchId into pro
							where q.IsDelete == null || q.IsDelete == false
							select new BranchDto
							{
								BranchName = q.BranchName,
								id = q.Id,
								CountProduct = pro.Count(),
								CreateDate = q.CreatedDate
							};

				if (searchDto != null)
				{
					if (!string.IsNullOrEmpty(searchDto.BranchName))
					{
						query = query.Where(delegate (BranchDto b)
						{
							return b.BranchName.RemoveAccentsUnicode().ToLower().Contains(searchDto.BranchName.RemoveAccentsUnicode().ToLower());
						}).AsQueryable();
					}
				}
				else
				{
					query = query.OrderByDescending(a => a.BranchName);
				}
				
				return query.ToList();
			}
			catch (Exception ex)
			{
				return null;
			}
		}

	}
}
