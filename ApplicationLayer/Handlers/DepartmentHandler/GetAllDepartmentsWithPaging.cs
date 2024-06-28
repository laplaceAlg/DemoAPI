using ApplicationLayer.Commons;
using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Departments;
using ApplicationLayer.Queries.DepartmentQuery;
using MediatR;

namespace ApplicationLayer.Handlers.DepartmentHandler
{
    public class GetAllDepartmentsWithPaging : IRequestHandler<GetAllDepartmentsWithPagingQuery, PaginatedList<DepartmentDto>>
    {
        private readonly IDepartment _department;
        public GetAllDepartmentsWithPaging(IDepartment department)
        {
            _department = department;
        }
        public async Task<PaginatedList<DepartmentDto>> Handle(GetAllDepartmentsWithPagingQuery request, CancellationToken cancellationToken)
        {
            var results = await _department.GetAllDepartmentsWithPaging(request.page, request.pageSize, request.searchValue);
           
            return results;
        }
    }
}
