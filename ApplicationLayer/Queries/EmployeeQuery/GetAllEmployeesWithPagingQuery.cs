using ApplicationLayer.Commons;
using ApplicationLayer.Dtos.Employees;
using DomainLayer.Entities;
using MediatR;

namespace ApplicationLayer.Queries.EmployeeQuery
{
    public class GetAllEmployeesWithPagingQuery : IRequest<PaginatedList<EmployeeDto>>
    {
        public int page {  get; set; }
        public int pageSize { get; set; }
        public string searchValue { get; set; } = string.Empty;
    }
}
