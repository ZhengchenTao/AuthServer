using AuthServer.Application.Users.Models;
using AuthServer.Entities;
using AuthServer.Exceptions;
using AuthServer.Persistence.Repository;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Application.Users.Commands
{
    public class CreateUserByIdentityCommandHandler : IRequestHandler<CreateUserByIdentityCommand, UserModel>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public CreateUserByIdentityCommandHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserModel> Handle(CreateUserByIdentityCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);
            return (await _repository.Add(user) > 0) ? _mapper.Map<UserModel>(user) :
                throw new FriendlyException(ErrorCode.CreateUserFailed);
        }
    }
}
