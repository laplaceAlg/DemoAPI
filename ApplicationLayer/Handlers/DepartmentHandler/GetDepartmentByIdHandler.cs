using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Departments;
using ApplicationLayer.Queries.DepartmentQuery;
using MediatR;

namespace ApplicationLayer.Handlers.DepartmentHandler
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        private readonly IDepartment _department;
        public GetDepartmentByIdHandler(IDepartment department)
        {
            _department = department;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _department.GetByIdAsync(request.Id);
        }
    }
}
