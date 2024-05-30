using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale.Domain.Entities;
using Sale.Service.Common;
using Sale.Service.Constant;
using Sale.Service.Dtos.BranchDto;
using Sale.Service.Dtos;
using AutoMapper;
using Sales.Model.Origin;
using Sale.Repository.OriginRepository;
using Sale.Service.OriginService;
using Sale.Service.Dtos.OriginDto;

namespace Sales.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OriginController : ControllerBase
	{
		private readonly IOriginService _originService;
		private readonly IMapper _mapper;
		public OriginController(IMapper mapper, IOriginService originService) { 
			_mapper = mapper;
			_originService = originService;
		}


		[HttpPost("create")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([FromForm] CreateVM entity)
		{
			try
			{
				var obj = new Origin();
				obj = _mapper.Map<Origin>(entity);
				await _originService.Create(obj);
				return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<Origin>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Thêm mới xuất xứ thành công"
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
		public async Task<IActionResult> GetDataByPage([FromForm] OriginSearchDto searchEntity)
		{

			try
			{
				var obj = await _originService.GetDataBypage(searchEntity);
				return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<PageList<OriginDto>?>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Lấy thương hiệu thành công"
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




		[HttpPost("get")]
		[AllowAnonymous]
		public async Task<IActionResult> getAll([FromBody] OriginSearchDto? searchEntity)
		{

			try
			{
				var obj = _originService.GetAll(searchEntity);
				return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<List<OriginDto>?>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Lấy thương hiệu thành công"
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
		public async Task<IActionResult> Edit([FromForm] EditVM entity)
		{

			try
			{
				var data = _originService.GetById(entity.id);
				if (data == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto
					{
						Status = StatusConstant.ERROR,
						Message = "Không tồn tại xuất xứ"
					});
				}
				else
				{
					var obj = _mapper.Map<Origin>(entity);
					await _originService.Update(obj);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Origin>
					{
						Data = obj,
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
				var data = _originService.GetById(Id);
				if (data == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto
					{
						Status = StatusConstant.ERROR,
						Message = "Không tồn tại xuất xứ"
					});
				}
				else
				{
					await _originService.Delete(data);
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Xóa xuất xứ thành công"
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
					var data = _originService.GetById(item);
					if (data != null)
					{
						await _originService.Delete(data);
					}
				}
				return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
				{
					Status = StatusConstant.SUCCESS,
					Message = "Xóa xuất xứ thành công"
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


	}
}
