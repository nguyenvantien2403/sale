using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Sale.Domain.Entities;
using Sale.Service.Dtos.ProductDto;
using Sales.Model.Product;

namespace Sales
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() {
			
			// Lấy ra tất cả Model controller
			var dto = typeof(ProductDto).Assembly.GetTypes().Where(t => !string.IsNullOrEmpty(t.Namespace) && t.Namespace.StartsWith("Sale.Service.Dtos") && t.Name.EndsWith("Dto"));
			//Láy ra entity
			var entityTypes = typeof(Product).Assembly.GetTypes().Where(t => !string.IsNullOrEmpty(t.Namespace) && t.Namespace.StartsWith("Sale.Domain.Entities"));
			//Lấy ra modelVM
			var modelVM  = typeof(CreateVM).Assembly.GetTypes().Where(t => !string.IsNullOrEmpty(t.Namespace) && t.Namespace.StartsWith("Sales.Model") && t.Name.EndsWith("VM"));

            foreach (var dtoType in dto)
            {
				var resdtoType = entityTypes.FirstOrDefault(e => e.Name == dtoType.Name.Substring(0,dtoType.Name.Length - 3));
				if (resdtoType != null)
				{
					CreateMap(dtoType, resdtoType);
					CreateMap(resdtoType, dtoType);
				}
			}         
			foreach (var model in modelVM)
            {
				var resdtoType = entityTypes.FirstOrDefault(e => e.Name == model.Namespace.Substring(model.Namespace.LastIndexOf(".") + 1));
				if (resdtoType != null)
				{
					CreateMap(model, resdtoType);
					CreateMap(resdtoType, model);
				}
			}
			

        }
	}
}
