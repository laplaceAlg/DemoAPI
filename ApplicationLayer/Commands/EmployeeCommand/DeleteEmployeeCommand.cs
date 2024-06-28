using MediatR;

namespace ApplicationLayer.Commands.EmployeeCommand
{
    public class DeleteEmployeeCommand :IRequest
    {
        public int Id { get; set; }
    }
}
