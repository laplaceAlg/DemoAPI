using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Employees;
using MediatR;

namespace ApplicationLayer.Commands.EmployeeCommand
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDto>
    {
        public CreateEmployeeDto? createEmployeeDto { get; set; }
    }
}
