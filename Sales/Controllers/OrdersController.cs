using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Constant;
using Sale.Service.Dtos;
using System.Security.Claims;
using Sale.Service.OrdersService;
using Sales.Model.Orders;
using Sale.Service.Dtos.OrdersDto;
using Sale.Service.ProductService;
using Sale.Service.Dtos.CartDto;

namespace Sales.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IOrdersService _ordersService;
		private readonly IProductService _productService;
		public OrdersController(IMapper mapper, IOrdersService ordersService, IProductService productService = null)
		{
			_mapper = mapper;
			_ordersService = ordersService;
			_productService = productService;
		}



		[HttpPost("create")]
		[AllowAnonymous]
		public async Task<IActionResult> Create([FromBody] CreateVM entity)
		{
			try
			{
				var obj = new Orders();
				obj = _mapper.Map<Orders>(entity);
				obj.CreatedDate = DateTime.Now;
				//xử lý upload
				await _ordersService.Create(obj);
				return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<Orders>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Thêm mới thương hiệu thành công"
				});
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
				{
					Status = StatusConstant.ERROR,
					Message = ex.Message
				});
			}
		}
		[HttpPost("getall")]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> GetDataByPage([FromBody] OrdersSearchDto searchEntity)
		{

			try
			{
				var obj = await _ordersService.GetDataByPage(searchEntity);
				return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<PageList<OrdersDto>?>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Lấy đơn hàng thành công"
				});
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
				{
					Status = StatusConstant.ERROR,
					Message = ex.Message
				});
			}


		}

		[HttpPut("Edit")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit([FromBody] string status , [FromQuery] Guid id)
		{

			try
			{
				var data = _ordersService.FindBy(x => x.Id == id).FirstOrDefault();
				if (data == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto
					{
						Status = StatusConstant.ERROR,
						Message = "Không tồn tại đơn hàng"
					});
				}
				else
				{
					var claims = HttpContext.User.Identity as ClaimsIdentity;
					if (claims != null)
					{
						var name = claims.FindFirst(ClaimTypes.Name);
						data.UpdatedBy = name.Value;
					}
					data.Status = status;
					data.UpdatedDate = DateTime.Now;
					await _ordersService.Update(data);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Orders>
					{
						Data = data,
						Status = StatusConstant.SUCCESS,
						Message = "Cập nhật sản phẩm thành công"
					});
				}

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
				{
					Status = StatusConstant.ERROR,
					Message = ex.Message
				});
			}
		}

		[HttpDelete("delete")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromQuery] Guid Id)
		{

			try
			{
				var data = _ordersService.GetById(Id);
				if (data == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Không tồn tại thương hiệu"
					});
				}
				else
				{
					await _ordersService.Delete(data);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Xóa sản phẩm thành công"
					});
				}


			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
				{
					Status = StatusConstant.ERROR,
					Message = ex.Message
				});
			}
		}



		[HttpDelete("delete-arrange")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteArange([FromBody] List<Guid> ListId)
		{
			try
			{
				foreach (var item in ListId)
				{
					var data = _ordersService.GetById(item);
					if (data != null)
					{
						await _ordersService.Delete(data);
					}
				}
				return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
				{
					Status = StatusConstant.SUCCESS,
					Message = "Xóa thương hiệu thành công"
				});

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
				{
					Status = StatusConstant.ERROR,
					Message = ex.Message
				});
			}


		}

		[HttpGet("detail")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Detail([FromQuery] Guid id)
		{

			try
			{
				var Orders = _ordersService.GetQueryable().Where(q => q.Id == id).Select(q => new OrdersDto
				{
					Id = q.Id,
					ShippingDate = q.ShippingDate,
					Createat = q.CreatedDate,
					address = q.address,
					statuscon = q.Status,
					Status = ConstantExtension.GetDisPlayConstant<OrdersConstant>(q.Status),
					mobile = q.mobile,
					totalPrice = q.totalPrice,
					firstName = q.firstName,
					lastName = q.lastName,
					email = q.email,
					orderNotes = q.orderNotes,
					cartDtos = q.Carts.Select(x => new CartDto
					{
						count = x.count,
						Id = x.Id,
						produtId = x.ProductId,
						ProductName = _productService.GetQueryable().Where(p => p.Id == x.ProductId).FirstOrDefault().ProductName,
						Price = _productService.GetQueryable().Where(p => p.Id == x.ProductId).FirstOrDefault().ProdcutPrice
					}).ToList()
				}).FirstOrDefault();
				if (Orders != null)
				{
					return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<OrdersDto>
					{
						Data = Orders,
						Status = StatusConstant.SUCCESS,
						Message = "Lấy thông tin đơn hàng thành công"
					});
				}
				else
				{
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Không tồn tại đơn hàng"
					});
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
				{
					Status = StatusConstant.ERROR,
					Message = ex.Message
				});
			}


		}
		[HttpPut("delete-soft")]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> DeleteSoft([FromQuery] Guid id)
		{

			try
			{
				var Orders = _ordersService.GetById(id);
				if (Orders == null)
				{
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Không tồn tại thương hiệu"
					});
				}
				else
				{
					var claims = HttpContext.User.Identity as ClaimsIdentity;
					if (claims != null)
					{
						var name = claims.FindFirst(ClaimTypes.Name);
						Orders.DeleteBy = name.Value;
					}
					Orders.IsDelete = true;
					await _ordersService.Update(Orders);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Orders>
					{
						Data = Orders,
						Status = StatusConstant.SUCCESS,
						Message = "Xóa thương hiệu thành công"
					});
				}

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
				{
					Status = StatusConstant.ERROR,
					Message = ex.Message
				});
			}
		}



	}
}
