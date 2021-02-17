using AuthServer.Entities;
using AuthServer.Persistence.Repository;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Application.Roles.Commands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public CreateRoleCommandHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Role>(request);
            return await _repository.Add(entity) > 0;
        }
    }
}
