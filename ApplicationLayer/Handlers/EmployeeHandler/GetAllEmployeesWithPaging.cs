using ApplicationLayer.Commons;
using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Employees;
using ApplicationLayer.Queries.EmployeeQuery;
using MediatR;

namespace ApplicationLayer.Handlers.EmployeeHandler
{
    public class GetAllDepartmentsWithPaging : IRequestHandler<GetAllEmployeesWithPagingQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IEmployee _employee;
        public GetAllDepartmentsWithPaging(IEmployee employee)
        {
            _employee = employee;
        }
        public async Task<PaginatedList<EmployeeDto>> Handle(GetAllEmployeesWithPagingQuery request, CancellationToken cancellationToken)
        {
            var results = await _employee.GetAllEmployeesWithPaging(request.page, request.pageSize, request.searchValue);
           
            return results;
        }
    }
}
