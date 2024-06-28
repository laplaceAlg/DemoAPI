using ApplicationLayer.Commands.EmployeeCommand;
using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Employees;
using MediatR;

namespace ApplicationLayer.Handlers.EmployeeHandler
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployee _employee;
        public CreateEmployeeHandler(IEmployee employee)
        {
            _employee = employee;
        }
        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employee.AddAsync(request.createEmployeeDto);
        }
    }
}
