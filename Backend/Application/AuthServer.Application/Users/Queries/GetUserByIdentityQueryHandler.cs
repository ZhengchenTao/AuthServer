using AuthServer.Application.Users.Models;
using AuthServer.Entities;
using AuthServer.Persistence.Repository;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Application.Users.Queries
{

    public class GetUserByIdentityQueryHandler : IRequestHandler<GetUserByIdentityQuery, UserModel>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetUserByIdentityQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserModel> Handle(GetUserByIdentityQuery request, CancellationToken cancellationToken)
        {
            var user = (await _repository.FindEntity<UserIdentity>(x =>
            x.Provider == request.Provider
            && x.Identity == request.Identity))?.User;
            return _mapper.Map<UserModel>(user);
        }
    }
}
