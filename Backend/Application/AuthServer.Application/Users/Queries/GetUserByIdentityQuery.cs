using AuthServer.Application.Users.Models;
using AuthServer.Enums;
using MediatR;

namespace AuthServer.Application.Users.Queries
{
    public class GetUserByIdentityQuery : IRequest<UserModel>
    {
        public IdentityProvider Provider { get; set; }

        public string Identity { get; set; }
    }
}
