using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Departments;
using ApplicationLayer.Queries.DepartmentQuery;
using MediatR;

namespace ApplicationLayer.Handlers.DepartmentHandler
{
    public class GetDepartmentListHandler : IRequestHandler<GetDepartmentListQuery, List<DepartmentDto>>
    {
        private readonly IDepartment _department;
        public GetDepartmentListHandler(IDepartment department)
        {
            _department = department;
        }
        public async Task<List<DepartmentDto>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            return await _department.GetAllAsync();
        }
    }
}
