
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Constant;
using Sale.Service.Dtos.PromotionDto;
using Sale.Service.Dtos;
using Sale.Service.RateService;
using System.Security.Claims;
using Sales.Model.Rate;
using Sale.Service.OrdersService;
using Microsoft.AspNetCore.Identity;
using Sale.Service.Dtos.RateDto;

namespace Sales.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RateController : ControllerBase
	{
		private readonly IRateService _rateService;
		private readonly IMapper _mapper;
		private readonly IOrdersService _ordersService;
		private readonly UserManager<IdentityUser> _usermanager;
		public RateController(IRateService rateService, IMapper mapper, IOrdersService ordersService, UserManager<IdentityUser> usermanager) {
			_mapper = mapper;
			_ordersService = ordersService;
			_usermanager = usermanager;
			_rateService = rateService;
		}


		[HttpPost("create")]
		[Authorize(Roles = "Admin,User")]
		public async Task<IActionResult> Create([FromForm] CreateVM entity)
		{
			try
			{
				var obj = new Rate();
				obj = _mapper.Map<Rate>(entity);
				obj.CreatedDate = DateTime.Now;
				var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

				if (claimsIdentity != null)
				{
					var name = claimsIdentity.FindFirst(ClaimTypes.Name);
					obj.CreatedBy = name.Value;
				}
				await _rateService.Create(obj);
				return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<Rate>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Thành công"
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
		[HttpPost("find-by-product")]
		public async Task<IActionResult> FindByID([FromQuery] Guid ProductId, [FromBody] RateSearchDto searchDto)
		{

			try
			{
				var data = _rateService.GetByProduct(ProductId,searchDto);
				return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<dynamic>
				{
					Data = data,
					Status = StatusConstant.SUCCESS,
					Message = "Thành công."
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
		[Authorize(Roles = "Admin,User")]
		public async Task<IActionResult> Edit([FromForm] EditVM entity, [FromQuery] Guid id)
		{
			try
			{
				var data = _rateService.FindBy(x => x.Id == id).FirstOrDefault();
				if (data == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto
					{
						Status = StatusConstant.ERROR,
						Message = "Không tồn tại"
					});
				}
				else
				{
					var claims = HttpContext.User.Identity as ClaimsIdentity;
					if (claims != null)
					{
						var name = claims.FindFirst(ClaimTypes.Name);
						data.UpdatedBy = name.Value;
						var user = await _usermanager.FindByNameAsync(name.Value);
						if (user != null)
						{
							if(user.Id == data.UserId.ToString())
							{
								data = _mapper.Map<EditVM, Rate>(entity, data);
								data.UpdatedDate = DateTime.Now;
								await _rateService.Update(data);
								return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Rate>
								{
									Data = data,
									Status = StatusConstant.SUCCESS,
									Message = "Cập nhật thành công"
								});
							}
							else
							{
								return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithDataDto<Rate>
								{
									Data = data,
									Status = StatusConstant.ERROR,
									Message = "Không thành công"
								});
							}
						}
						else
						{
							return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithDataDto<Rate>
							{
								Data = data,
								Status = StatusConstant.ERROR,
								Message = "Không thành công"
							});
						}

					} else
					{
						return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Rate>
						{
							Data = data,
							Status = StatusConstant.SUCCESS,
							Message = "Yêu cầu đăng nhập"
						});
					}
					
					
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
		[Authorize(Roles = "Admin,User")]
		public async Task<IActionResult> Delete([FromQuery] Guid Id)
		{

			try
			{
				var data = _rateService.GetById(Id);
				if (data == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Không tồn tại đánh giá"
					});
				}
				else
				{
					await _rateService.Delete(data);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Xóa đánh giá thành công"
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
