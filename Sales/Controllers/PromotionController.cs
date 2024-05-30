using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Constant;
using Sale.Service.Dtos;
using Sale.Service.Dtos.PromotionDto;
using Sale.Service.PromotionService;
using Sales.Model.Promotion;
using System.Security.Claims;

namespace Sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
		private readonly IMapper _mapper;
		private readonly IPromotionService _promotionService;
		public PromotionController(
			IMapper mapper,
			IPromotionService promotionService

			)
		{
			_mapper = mapper;
			_promotionService = promotionService;
		}



		[HttpPost("create")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([FromForm] CreateVM entity)
		{
			try
			{
				var obj = new Promotion();
				obj = _mapper.Map<Promotion>(entity);
				obj.CreatedDate = DateTime.Now;

				var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
				if (claimsIdentity != null)
				{
					var name = claimsIdentity.FindFirst(ClaimTypes.Name);
					obj.CreatedBy = name.Value;
				}


				await _promotionService.Create(obj);


				return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<Promotion>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Thêm mới khuyến mại thành công"
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
		[AllowAnonymous]
		public async Task<IActionResult> GetDataByPage([FromBody] PromotionSearchDto searchEntity)
		{
			try
			{
				var obj = await _promotionService.GetDataByPage(searchEntity);
				return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<PageList<PromotionDto>?>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Lấy khuyến mãi thành công"
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

		/// <summary>
		/// Cập nhật
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPut("Edit")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit([FromForm] EditVM entity, [FromQuery] Guid id)
		{

			try
			{
				var data = _promotionService.FindBy(x => x.Id == id).FirstOrDefault();
				if (data == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto
					{
						Status = StatusConstant.ERROR,
						Message = "Không tồn tại khuyến mãi"
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
					data = _mapper.Map<EditVM, Promotion>(entity, data);
					data.UpdatedDate = DateTime.Now;
					await _promotionService.Update(data);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Promotion>
					{
						Data = data,
						Status = StatusConstant.SUCCESS,
						Message = "Cập nhật khuyến mãi thành công"
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
				var data = _promotionService.GetById(Id);
				if (data == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Không tồn tại khuyến mãi"
					});
				}
				else
				{
					await _promotionService.Delete(data);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Xóa khuyến mãi thành công"
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
					var data = _promotionService.GetById(item);
					if (data != null)
					{
						await _promotionService.Delete(data);
					}
				}
				return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
				{
					Status = StatusConstant.SUCCESS,
					Message = "Xóa khuyến mãi thành công"
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
		[AllowAnonymous]
		public async Task<IActionResult> Detail([FromQuery] Guid id)
		{

			try
			{
				var promotion = _promotionService.FindBy(x => x.Id == id).FirstOrDefault();
				if (promotion != null)
				{
					return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Promotion>
					{
						Data = promotion,
						Status = StatusConstant.SUCCESS,
						Message = "Lấy thông tin khuyến mãi thành công"
					});
				}
				else
				{
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Không tồn tại khuyến mãi"
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
				var promo = _promotionService.GetById(id);
				if (promo == null)
				{
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Không tồn tại khuyến mãi"
					});
				}
				else
				{
					var claims = HttpContext.User.Identity as ClaimsIdentity;
					if (claims != null)
					{
						var name = claims.FindFirst(ClaimTypes.Name);
						promo.DeleteBy = name.Value;
					}
					promo.IsDelete = true;
					promo.DeleteTime = DateTime.Now;
					await _promotionService.Update(promo);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Promotion>
					{
						Data = promo,
						Status = StatusConstant.SUCCESS,
						Message = "Xóa khuyến mại thành công"
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
