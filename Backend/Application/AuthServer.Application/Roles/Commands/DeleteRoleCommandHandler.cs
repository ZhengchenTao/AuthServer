using AuthServer.Entities;
using AuthServer.Persistence.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Application.Roles.Commands
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRepository _repository;

        public DeleteRoleCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _repository.Remove<Role>(request.Id) > 0;
        }
    }
}
