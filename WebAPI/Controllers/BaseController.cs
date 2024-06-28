using ApplicationLayer.Commands;
using ApplicationLayer.Commons;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /*   public class BaseController : Controller
       {
           protected IActionResult HandleException<T>(AppException ex) where T : class
           {
               switch (ex.ErrorCode)
               {
                   case 204:
                       var response = ApiResponse<T>.CreateNotFound(ex.Message);
                       return NotFound(new { result = response.Data, success = response.Success, error = response.Error });

                   case 404:
                       var response1 = ApiResponse<T>.CreateNotFound(ex.Message);
                       return NotFound(new { result = response1.Data, success = response1.Success, error = response1.Error });
                   case 400:
                       var response2 = ApiResponse<T>.CreateBadRequest(ex.Message);
                       return BadRequest(new { result = response2.Data, success = response2.Success, error = response2.Error });
                   default:
                       var errorResponse = ApiResponse<T>.CreateError(ex.Message);
                       return StatusCode(StatusCodes.Status500InternalServerError, new { result = errorResponse.Data, success = errorResponse.Success, error = errorResponse.Error });
               }
     *//*          if (ex.Message.Contains("not found"))
               {
                   var response = ApiResponse<T>.CreateNotFound(ex.Message);
                   return NotFound(new { result = response.Data, success = response.Success, error = response.Error });
               }
               if (ex.Message.Contains("already exist"))
               {
                   var response = ApiResponse<T>.CreateBadRequest(ex.Message);
                   return BadRequest(new { result = response.Data, success = response.Success, error = response.Error});
               }

               var errorResponse = ApiResponse<T>.CreateError(ex.Message);
               return StatusCode(StatusCodes.Status500InternalServerError, new { result = errorResponse.Data, success = errorResponse.Success, error = errorResponse.Error});*//*
           }

       }*/
    public class BaseController : Controller
    {
        protected IActionResult OkResult<T>(T data, string resultObjectName)
        {
          
            var response = ApiResponse<T>.CreateSuccess(data);
            var resultObject = new Dictionary<string, object>
                {
                    { resultObjectName, response.Data }
                };


            return Ok(new
            {
                result = resultObject,
                success = response.Success,
                error = response.Error
            });
        }

        protected IActionResult OkResultPaging<T>(T data)
        {
            var response = ApiResponse<T>.CreateSuccess(data);

            return Ok(new
            {
                result = response.Data,
                success = response.Success,
                error = response.Error
            });
        }

        protected IActionResult HandleException<T>(AppException ex)
        {
            switch (ex.ErrorCode)
            {
                case 204:
                    var response204 = ApiResponse<T>.CreateNoContent();
                    return NotFound(new { result = response204.Data, success = response204.Success, error = response204.Error });
                case 404:
                    var response404 = ApiResponse<T>.CreateNotFound(ex.Message);
                    return NotFound(new { result = response404.Data, success = response404.Success, error = response404.Error });
                case 400:
                    var response400 = ApiResponse<T>.CreateBadRequest(ex.Message);
                    return BadRequest(new { result = response400.Data, success = response400.Success, error = response400.Error });
                default:
                    var errorResponse = ApiResponse<T>.CreateError(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, new { result = errorResponse.Data, success = errorResponse.Success, error = errorResponse.Error });
            }
        }
    }

}
