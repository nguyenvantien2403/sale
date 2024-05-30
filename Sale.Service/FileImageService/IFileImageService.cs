using Sale.Domain.Entities;
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
	public interface IFileImageService : IService<FileImage>
	{
		Task<PageList<FileImageDto>> GetDataByPage(FileImageSearchDto searchDto);
	 	List<FileImageDto>? FindByProductID(Guid productId);
	 	List<FileImage>? FindByProductIDv2(Guid productId);

		
	}
}
