using AuthServer.Application.Mapping;
using AuthServer.Entities;
using AutoMapper;
using MediatR;

namespace AuthServer.Application.Roles.Commands
{
    public class CreateRoleCommand : IRequest<bool>, IMappingProfile
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateRoleCommand, Role>();
        }
    }
}
