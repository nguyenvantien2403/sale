using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale.Service.Constant;
using Sale.Service.Dtos;
using Sale.Service.SastisService;

namespace Sales.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SastisController : ControllerBase
	{

		private readonly ISastisService _sastisService;

		public SastisController(ISastisService sastisService = null)
		{
			_sastisService = sastisService;
		}



		[HttpGet("dashboard")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DashboardInformation()
		{
			try
			{
				var data = _sastisService.GetAllData();
				return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<dynamic>
				{
					Message = "Thong ke thanh cong",
					Data = data,
					Status = StatusConstant.SUCCESS
				});

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
				{
					Message = ex.Message,
					Status = StatusConstant.ERROR
				});
			}
		}
	}

}
