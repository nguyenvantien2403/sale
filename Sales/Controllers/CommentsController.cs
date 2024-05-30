using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sale.Domain.Entities;
using Sale.Service.CommentsService;
using Sale.Service.Constant;
using Sale.Service.Dtos;
using Sale.Service.Dtos.CommentDto;
using Sales.Model.Comments;
using System.Security.Claims;

namespace Sales.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{


		private readonly IMapper _mapper;
		private readonly ICommentsService _commentsService;
		private readonly UserManager<IdentityUser> _usermanager;

		public CommentsController(ICommentsService commentsService, IMapper mapper, UserManager<IdentityUser> usermanager) {
			_commentsService = commentsService;
			_mapper = mapper;
			_usermanager = usermanager;
		}

		[AllowAnonymous]
		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] CreateVM entity)
		{
			try
			{
				var obj = new Comments();
				obj = _mapper.Map<Comments>(entity);
				obj.CreatedDate = DateTime.Now;
				var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
				if (claimsIdentity.IsAuthenticated)
				{
					var name = claimsIdentity.FindFirst(ClaimTypes.Name);
					obj.CreatedBy = name.Value;
				}
				await _commentsService.Create(obj);
				return StatusCode(StatusCodes.Status201Created, new ResponseWithDataDto<Comments>
				{
					Data = obj,
					Status = StatusConstant.SUCCESS,
					Message = "Comment successfully"
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

		[AllowAnonymous]
		[HttpGet("find-by-product")]
		public async Task<IActionResult> FindByID([FromQuery] Guid ProductID)
		{
			try
			{
				var data = _commentsService.GetByProduct(ProductID);
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
				var data = _commentsService.FindBy(x => x.Id == id).FirstOrDefault();
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
							if (user.Id == data.UserId.ToString())
							{
								data = _mapper.Map<EditVM, Comments>(entity, data);
								data.UpdatedDate = DateTime.Now;
								await _commentsService.Update(data);
								return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Comments>
								{
									Data = data,
									Status = StatusConstant.SUCCESS,
									Message = "Cập nhật thành công"
								});
							}
							else
							{
								return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithDataDto<Comments>
								{
									Data = data,
									Status = StatusConstant.ERROR,
									Message = "Không thành công"
								});
							}
						}
						else
						{
							return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithDataDto<Comments>
							{
								Data = data,
								Status = StatusConstant.ERROR,
								Message = "Không thành công"
							});
						}

					}
					else
					{
						return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<Comments>
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
				var data = _commentsService.GetById(Id);
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
					await _commentsService.Delete(data);
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
