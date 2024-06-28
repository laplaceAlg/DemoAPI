using ApplicationLayer.Commands.DepartmentCommand;
using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Departments;
using MediatR;

namespace ApplicationLayer.Handlers.DepartmentHandler
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
    {
        private readonly IDepartment _department;
        public UpdateDepartmentHandler(IDepartment department)
        {
            _department = department;
        }
        public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await _department.UpdateAsync(request.departmentDto);
        }
    }
}

