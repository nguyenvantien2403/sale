using Microsoft.AspNetCore.Http.HttpResults;
using Sale.Domain.Entities;
using Sale.Repository.FileImageRepository;
using Sale.Service.Common;
using Sale.Service.Core;
using Sale.Service.Dtos.FileImageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.FileImageService
{
	public class FileImageService : Service<FileImage> , IFileImageService
	{
		private readonly IFileImageRepository _fileImageRepository;
		public FileImageService(IFileImageRepository fileImageRepository) : base(fileImageRepository) {
		
		_fileImageRepository = fileImageRepository;
		
		}

		public List<FileImageDto>? FindByProductID(Guid productId)
		{
			try
			{
				var query = from file in _fileImageRepository.GetQueryable()
							where file.ProductId == productId && (file.IsDelete == false || file.IsDelete == null)
							select new FileImageDto
							{
								ProductId = productId,
								extension = file.extension,
								FilePath = file.FilePath,
								mime = file.mime,
								CreateAt = file.CreatedDate
							};
				return query.ToList();
			}
			catch (Exception ex)
			{

				return null;
			}
		}

		public List<FileImage>? FindByProductIDv2(Guid productId)
		{
			try
			{
				var query = from file in _fileImageRepository.GetQueryable()
							where file.ProductId == productId && (file.IsDelete == false || file.IsDelete == null)
							select new FileImage
							{
								ProductId = productId,
								extension = file.extension,
								FilePath = file.FilePath,
								mime = file.mime,
							};
				return query.ToList();
			}
			catch (Exception ex)
			{

				return null;
			}
		}

		public async Task<PageList<FileImageDto>> GetDataByPage(FileImageSearchDto searchDto)
		{
			try
			{
				var query = from file in _fileImageRepository.GetQueryable()
							select new FileImageDto
							{
								extension = file.extension,
								ProductId = file.ProductId,
								FilePath = file.FilePath,
								mime = file.mime,
								CreateAt = file.CreatedDate
							};

				if(searchDto != null)
				{
					if(searchDto.ProductId != null)
					{
						query = query.Where(x => x.ProductId == searchDto.ProductId);
					}
				}
				var data = PageList<FileImageDto>.Cretae(query, searchDto);
				return data;
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}



}
