using AuthServer.Entities;
using AuthServer.Persistence.Repository;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Application.Roles.Commands
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public UpdateRoleCommandHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindEntity<Role>(request.Id);
            _mapper.Map(request, entity);
            return await _repository.Update(entity) > 0;
        }
    }
}
