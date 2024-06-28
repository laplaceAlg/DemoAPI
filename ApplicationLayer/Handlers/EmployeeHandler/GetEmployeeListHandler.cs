using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Employees;
using ApplicationLayer.Queries.EmployeeQuery;
using MediatR;

namespace ApplicationLayer.Handlers.EmployeeHandler
{
    public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeDto>>
    {
        private readonly IEmployee _employee;
        public GetEmployeeListHandler( IEmployee employee)
        {
            _employee = employee;
        }
        public async Task<List<EmployeeDto>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await _employee.GetAllAsync();
        }
    }
}
