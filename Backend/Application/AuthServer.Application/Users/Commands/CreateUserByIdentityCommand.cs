using AuthServer.Application.Users.Models;
using MediatR;

namespace AuthServer.Application.Users.Commands
{
    public class CreateUserByIdentityCommand : IRequest<UserModel>
    {
        public UserModel User { get; set; }
    }
}
