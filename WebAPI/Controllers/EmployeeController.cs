using ApplicationLayer.Commands;
using ApplicationLayer.Commands.EmployeeCommand;
using ApplicationLayer.Commons;
using ApplicationLayer.Dtos.Employees;
using ApplicationLayer.Queries.EmployeeQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWithPaging(int page = 1, int pageSize = 10, string searchValue = "")
        {
            var data = await mediator.Send(new GetAllEmployeesWithPagingQuery { page = page, pageSize = pageSize, searchValue = searchValue});

            //if (data.Count == 0)
            //{
            //    var result = ApiResponse<PaginatedList<EmployeeDto>>.CreateNotFound($"Employee not available");
            //    return NotFound(new { result = result.Data, success = result.Success, error = result.Error });
            //    /*return NotFound(ApiResponse<List<Employee>>.CreateNotFound("Employee not available"));*/
            //}

            /*return Ok(response);*/
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
            return Ok(new { result =  response, success = response1.Success, error = response1.Error } );
        }



        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await mediator.Send(new GetEmployeeListQuery());
            /*  if (data.Count() == 0)
              {
                  return NotFound("Employee not available");
              }
              return Ok(data);*/
            if (data.Count == 0)
            {
                var result = ApiResponse<EmployeeDto>.CreateNotFound($"Employee not available");
                return NotFound(new { result = result.Data, success = result.Success, error = result.Error });
            }
            var response = ApiResponse<List<EmployeeDto>>.CreateSuccess(data);

            return Ok(new { result = new { employees = response.Data }, success = response.Success, error = response.Error  });
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var data = await mediator.Send(new GetEmployeeByIdQuery { Id = id });
       /*     if (data == null)
                return NotFound($"Employee not found with id {id}");
            return Ok(data);*/
            if(data == null)
            {
                var response1 = ApiResponse<EmployeeDto>.CreateNotFound($"Employee not found with id {id}");
                return NotFound( new { result = response1.Data , success = response1.Success, error = response1.Error} );
            }
            var response = ApiResponse<EmployeeDto>.CreateSuccess(data);
            return Ok(new { result = new { employee = response.Data }, success =response.Success, error = response.Error} );
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                var data = await mediator.Send(new CreateEmployeeCommand { createEmployeeDto = createEmployeeDto });
                var response = ApiResponse<EmployeeDto>.CreateSuccess(data);
                return Ok(new { result = new { employee = response.Data }, success = response.Success, error = response.Error });
            }
            catch (Exception ex)
            {
                var response = ApiResponse<EmployeeDto>.CreateError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = response.Data, success = response.Success, error = response.Error });
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            /*var data = await mediator.Send(new UpdateEmployeeCommand { createEmployeeDto = createEmployeeDto });
            return Ok(data);*/
            try
            {
                var data = await mediator.Send(new UpdateEmployeeCommand { createEmployeeDto = createEmployeeDto });
                var response = ApiResponse<EmployeeDto>.CreateSuccess(data);
                return Ok(new { result = new { employee = response.Data }, success = response.Success, error = response.Error });
            }
            catch (Exception ex)
            {
                var response = ApiResponse<EmployeeDto>.CreateError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = response.Data, success = response.Success, error = response.Error });
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteEmployeeCommand { Id = id });
                var response = ApiResponse<Task>.CreateSuccess(null);
                return Ok(new { result = response.Data, success = response.Success, error = response.Error });
            }
            catch (Exception ex)
            {
                var response = ApiResponse<Task>.CreateError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = response.Data, success = response.Success, error = response.Error });
            }
       
            /*return Ok(data);*/
        }
    }
}
