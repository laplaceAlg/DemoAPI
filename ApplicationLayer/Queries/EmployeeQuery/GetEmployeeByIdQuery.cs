using ApplicationLayer.Dtos.Employees;
using MediatR;

namespace ApplicationLayer.Queries.EmployeeQuery
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int Id { get; set; }
    }
}
