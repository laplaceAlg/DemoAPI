using ApplicationLayer.Commands.DepartmentCommand;
using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Departments;
using MediatR;

namespace ApplicationLayer.Handlers.DepartmentHandler
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
    {
        private readonly IDepartment _department;
        public CreateDepartmentHandler(IDepartment department)
        {
            _department = department;
        }
        public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await _department.AddAsync(request.departmentDto);
        }
    }
}
