using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Employees;
using MediatR;

namespace ApplicationLayer.Commands.EmployeeCommand
{
    public class CreateEmployeeCommand : IRequest<EmployeeDto>
    {
        public CreateEmployeeDto? createEmployeeDto { get; set; }
    }
}
