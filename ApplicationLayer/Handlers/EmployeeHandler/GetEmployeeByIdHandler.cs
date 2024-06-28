using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Employees;
using ApplicationLayer.Queries.EmployeeQuery;
using MediatR;

namespace ApplicationLayer.Handlers.EmployeeHandler
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IEmployee _employee;
        public GetEmployeeByIdHandler(IEmployee employee)
        {
            _employee = employee;
        }
        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _employee.GetByIdAsync(request.Id);
        }
    }
}
