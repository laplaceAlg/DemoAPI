using ApplicationLayer.Commands.DepartmentCommand;
using ApplicationLayer.Contracts;
using MediatR;

namespace ApplicationLayer.Handlers.DepartmentHandler
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IDepartment _department;
        public DeleteDepartmentHandler(IDepartment department)
        {
            _department = department;
        }
        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
           await _department.DeleteAsync(request.Id);
        }
    }
}
