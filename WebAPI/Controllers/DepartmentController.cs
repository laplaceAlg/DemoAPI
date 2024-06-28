using ApplicationLayer.Commands;
using ApplicationLayer.Commands.DepartmentCommand;
using ApplicationLayer.Commons;
using ApplicationLayer.Dtos.Departments;
using ApplicationLayer.Queries.DepartmentQuery;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class DepartmentController : BaseController
    {
        private readonly IMediator mediator;
 
        public DepartmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

      /// <summary>
      /// Get List Department With Paging
      /// </summary>
      /// <param name="page"></param>
      /// <param name="pageSize"></param>
      /// <param name="searchValue"></param>
      /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllWithPaging(int page = 1, int pageSize = 10, string searchValue = "")
        {
           

            /*if (data.Count == 0)
            {
                var result = ApiResponse<PaginatedList<DepartmentDto>>.CreateNotFound($"Departments not available");
                return NotFound(new { result = result.Data, success = result.Success, error = result.Error });
            }*/
           try
            {
                var data = await mediator.Send(new GetAllDepartmentsWithPagingQuery { page = page, pageSize = pageSize, searchValue = searchValue });
                
                var response = new
                {
                    departments = data,
                    totalCount = data.TotalCount,
                    currentPage = data.CurrentPage,
                    totalPages = data.TotalPages,
                    pageSize = data.PageSize,
                    hasPrevious = data.HasPrevious,
                    hasNext = data.HasNext
                };
                return OkResultPaging(response);
            } 
            catch (AppException ex)
            {
                return HandleException<List<DepartmentDto>>(ex);
            }
           /* var data = await mediator.Send(new GetAllDepartmentsWithPagingQuery { page = page, pageSize = pageSize, searchValue = searchValue });
            var response1 = ApiResponse<PaginatedList<DepartmentDto>>.CreateSuccess(data);
            var response = new
            {
                departments = response1.Data,
                totalCount = data.TotalCount,
                currentPage = data.CurrentPage,
                totalPages = data.TotalPages,
                pageSize = data.PageSize,
                hasPrevious = data.HasPrevious,
                hasNext = data.HasNext
            };
            return Ok(new { result = response, success = response1.Success, error = response1.Error });*/
        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            /* var data = await mediator.Send(new GetDepartmentListQuery());
             if (data.Count == 0)
             {
                 var result = ApiResponse<DepartmentDto>.CreateNotFound($"Department not available");
                 return NotFound(new { result = result.Data, success = result.Success, error = result.Error });
             }
             var response = ApiResponse<List<DepartmentDto>>.CreateSuccess(data);

             return Ok(new { result = new { departments = response.Data }, success = response.Success, error = response.Error });*/
            try
            {
                var data = await mediator.Send(new GetDepartmentListQuery());
                return OkResult(data, "departments");
            }
            catch (AppException ex)
            {
                return HandleException<DepartmentDto>(ex);
            }
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {

                var data = await mediator.Send(new GetDepartmentByIdQuery { Id = id });
                return OkResult(data, "department");
            }
            catch (AppException ex)
            {
                return HandleException<DepartmentDto>(ex);
            }
        }


        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDepartmentDto departmentDto)
        {
            /* var data = await mediator.Send(new CreateDepartmentCommand { departmentDto = departmentDto });
             return Ok(data);*/
            try
            {
                var data = await mediator.Send(new CreateDepartmentCommand { departmentDto = departmentDto });
                var response = ApiResponse<DepartmentDto>.CreateSuccess(data);
                return Ok(new { result = new { department = response.Data }, success = response.Success, error = response.Error });
            }
            catch (Exception ex)
            {
                var response1 = ApiResponse<DepartmentDto>.CreateError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = response1.Data, success = response1.Success, error = response1.Error });
            }
        }

        // PUT api/<DepartmentController>/5
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] CreateDepartmentDto departmentDto)
        {
            try
            {
                var updatedDepartment = await mediator.Send(new UpdateDepartmentCommand { departmentDto = departmentDto });
                return OkResult(updatedDepartment, "department");
            }
            catch (AppException ex)
            {
                return HandleException<DepartmentDto>(ex);
            }
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteDepartmentCommand { Id = id });
                var response = ApiResponse<Task>.CreateSuccess(null);
                return Ok(new { result = response.Data, success = response.Success, error = response.Error });
            }
            catch (Exception ex)
            {
                var response = ApiResponse<Task>.CreateError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = response.Data, success = response.Success, error = response.Error });
            }

            /*var data = await mediator.Send(new DeleteDepartmentCommand { Id = id });
            return Ok(data);*/
        }
       
    }
}
