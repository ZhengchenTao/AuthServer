using AuthServer.Application.Roles.Models;
using AuthServer.Entities;
using AuthServer.Persistence.Repository;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Application.Roles.Queries
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleModel>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetRoleQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RoleModel> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindEntity<Role>(request.Id);
            return _mapper.Map<RoleModel>(entity);
        }
    }
}
