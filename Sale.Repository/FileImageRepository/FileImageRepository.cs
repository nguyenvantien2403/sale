using Sale.Domain;
using Sale.Domain.Entities;
using Sale.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Repository.FileImageRepository
{
	public class FileImageRepository : Repository<FileImage>, IFileImageRepository
	{
		public FileImageRepository(SaleContext context) : base(context)
		{
		}
	}
}
