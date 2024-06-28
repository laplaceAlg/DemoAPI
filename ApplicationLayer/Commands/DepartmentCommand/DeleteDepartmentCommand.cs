using ApplicationLayer.Dtos.Departments;
using MediatR;

namespace ApplicationLayer.Commands.DepartmentCommand
{
    public class DeleteDepartmentCommand :IRequest
    {
        public int Id { get; set; }
    }
}
