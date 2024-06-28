using ApplicationLayer.Commons;
using ApplicationLayer.Dtos.Departments;
using MediatR;

namespace ApplicationLayer.Queries.DepartmentQuery
{
    public class GetAllDepartmentsWithPagingQuery : IRequest<PaginatedList<DepartmentDto>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string searchValue { get; set; } = string.Empty;
    }
}
