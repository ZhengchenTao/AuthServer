using AuthServer.Application.Roles.Models;
using MediatR;
using System;

namespace AuthServer.Application.Roles.Queries
{
    public class GetRoleQuery : IRequest<RoleModel>
    {
        public Guid Id { get; set; }

        public GetRoleQuery(Guid id)
        {
            Id = id;
        }
    }
}
