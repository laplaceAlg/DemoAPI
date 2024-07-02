using ApplicationLayer.Commands.EmployeeCommand;
using ApplicationLayer.Commons;
using ApplicationLayer.Dtos.Departments;
using ApplicationLayer.Dtos.Employees;
using ApplicationLayer.Queries.EmployeeQuery;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class EmployeeController : BaseController
    {
        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWithPaging(int page = 1, int pageSize = 10, string searchValue = "")
        {
            /* var data = await mediator.Send(new GetAllEmployeesWithPagingQuery { page = page, pageSize = pageSize, searchValue = searchValue});
             var response1 = ApiResponse<PaginatedList<EmployeeDto>>.CreateSuccess(data);
             var response = new
             {
                 employees = response1.Data,
                 totalCount = data.TotalCount,
                 currentPage = data.CurrentPage,
                 totalPages = data.TotalPages,
                 pageSize = data.PageSize,
                 hasPrevious = data.HasPrevious,
                 hasNext = data.HasNext
             };
             return Ok(new { result =  response, success = response1.Success, error = response1.Error } );*/
            try
            {
                var data = await mediator.Send(new GetAllEmployeesWithPagingQuery { page = page, pageSize = pageSize, searchValue = searchValue });

                var response = new
                {
                    employees = data,
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
        }



        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await mediator.Send(new GetEmployeeListQuery());
                return OkResult(data, "employees");
            }
            catch (AppException ex)
            {
                return HandleException<List<EmployeeDto>>(ex);
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                var data= await mediator.Send(new GetEmployeeByIdQuery { Id = id });
                return OkResult(data, "employee");
            }
            catch (AppException ex)
            {
                return HandleException<EmployeeDto>(ex);
            }
          /*  var data = await mediator.Send(new GetEmployeeByIdQuery { Id = id });
            if(data == null)
            {
                var response1 = ApiResponse<EmployeeDto>.CreateNotFound($"Employee not found with id {id}");
                return NotFound( new { result = response1.Data , success = response1.Success, error = response1.Error} );
            }
            var response = ApiResponse<EmployeeDto>.CreateSuccess(data);
            return Ok(new { result = new { employee = response.Data }, success =response.Success, error = response.Error} );*/
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeDto createEmployeeDto)
        {
           /* try
            {
                var data = await mediator.Send(new CreateEmployeeCommand { createEmployeeDto = createEmployeeDto });
                var response = ApiResponse<EmployeeDto>.CreateSuccess(data);
                return Ok(new { result = new { employee = response.Data }, success = response.Success, error = response.Error });
            }
            catch (Exception ex)
            {
                var response = ApiResponse<EmployeeDto>.CreateError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = response.Data, success = response.Success, error = response.Error });
            }*/
            try
            {
                var data = await mediator.Send(new CreateEmployeeCommand { createEmployeeDto = createEmployeeDto });
                return OkResult(data, "employee");
            }
            catch (AppException ex)
            {
                return HandleException<EmployeeDto>(ex);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                var data = await mediator.Send(new UpdateEmployeeCommand { createEmployeeDto = createEmployeeDto });
                return OkResult(data, "employee");
            }
            catch (AppException ex)
            {
                return HandleException<EmployeeDto>(ex);
            }
            /*try
            {
                var data = await mediator.Send(new UpdateEmployeeCommand { createEmployeeDto = createEmployeeDto });
                var response = ApiResponse<EmployeeDto>.CreateSuccess(data);
                return Ok(new { result = new { employee = response.Data }, success = response.Success, error = response.Error });
            }
            catch (Exception ex)
            {
                var response = ApiResponse<EmployeeDto>.CreateError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = response.Data, success = response.Success, error = response.Error });
            }*/
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteEmployeeCommand { Id = id });
                return OkResult<object>(null, null);
                /*return OkResultPaging(new { message = "Employee deleted successfully." });*/
            }
            catch (AppException ex)
            {
                return HandleException<Task>(ex);
            }
        }
    }
}
