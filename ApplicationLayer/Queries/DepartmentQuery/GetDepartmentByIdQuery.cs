using ApplicationLayer.Dtos.Departments;
using MediatR;

namespace ApplicationLayer.Queries.DepartmentQuery
{
    public class GetDepartmentByIdQuery : IRequest<DepartmentDto>
    {
        public int Id { get; set; }
    }
}
