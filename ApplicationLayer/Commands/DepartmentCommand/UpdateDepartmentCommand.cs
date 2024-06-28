using ApplicationLayer.Dtos.Departments;
using MediatR;

namespace ApplicationLayer.Commands.DepartmentCommand
{
    public class UpdateDepartmentCommand : IRequest<DepartmentDto>
    {
        public CreateDepartmentDto? departmentDto { get; set; }
    }
}
