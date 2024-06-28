using ApplicationLayer.Dtos.Employees;
using MediatR;

namespace ApplicationLayer.Queries.EmployeeQuery
{
    public class GetEmployeeListQuery : IRequest<List<EmployeeDto>>
    {
    }
}
