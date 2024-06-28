using ApplicationLayer.Commands.EmployeeCommand;
using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Employees;
using MediatR;

namespace ApplicationLayer.Handlers.EmployeeHandler
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployee _employee;
        public UpdateEmployeeHandler(IEmployee employee)
        {
            _employee = employee;
        }
        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employee.UpdateAsync(request.createEmployeeDto);
        }
    }
}

