using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale.Service.Constant;
using Sale.Service.Dtos;
using Sale.Service.Dtos.FileImageDto;
using Sale.Service.FileImageService;
using Sale.Service.ProductService;

namespace Sales.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileImageController : ControllerBase
	{

        private readonly IFileImageService _fileImageService;
        public  FileImageController(IFileImageService fileImageService)
        {
           _fileImageService = fileImageService;    
        }

        /// <summary>
        /// get fiel and image by productID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
		[HttpGet("Get-By-Id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFileById([FromQuery] Guid productId)
        {
            try 
            {
                var fileImage = _fileImageService.FindByProductID(productId);
                if(fileImage != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<List<FileImageDto>>
                    {
                        Data = fileImage,
                        Status = StatusConstant.SUCCESS,
                        Message = "Lấy thông tin thành công"
                    });
                }
                else
                {
					return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
					{
						Status = StatusConstant.SUCCESS,
						Message = "Lấy thông tin thành công"
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

        [HttpDelete("delete-by-ProductId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] List<Guid> listId)
        {
            try
            {
                foreach (var item in listId)
                {
                    var data = _fileImageService.FindBy(x => x.ProductId == item && (x.IsDelete == false || x.IsDelete == null)).ToList();
                    if(data != null)
                    {
                       await _fileImageService.Delete(data);
                    } 
                }
                return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto
				{
					Status = StatusConstant.SUCCESS,
					Message = "Xóa file thành công"
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
