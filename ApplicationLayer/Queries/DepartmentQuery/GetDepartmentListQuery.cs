using ApplicationLayer.Dtos.Departments;
using MediatR;

namespace ApplicationLayer.Queries.DepartmentQuery
{
    public class GetDepartmentListQuery : IRequest<List<DepartmentDto>>
    {
    }
}
