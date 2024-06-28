using ApplicationLayer.Commands.EmployeeCommand;
using ApplicationLayer.Contracts;
using MediatR;

namespace ApplicationLayer.Handlers.EmployeeHandler
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployee _employee;
        public DeleteEmployeeHandler(IEmployee employee)
        {
            _employee = employee;
        }
        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
             await _employee.DeleteAsync(request.Id);
        }
    }
}
