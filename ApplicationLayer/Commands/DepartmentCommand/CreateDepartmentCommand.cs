using ApplicationLayer.Dtos.Departments;
using MediatR;

namespace ApplicationLayer.Commands.DepartmentCommand
{
    public class CreateDepartmentCommand : IRequest<DepartmentDto>
    {
        public CreateDepartmentDto? departmentDto { get; set; }
    }
}
