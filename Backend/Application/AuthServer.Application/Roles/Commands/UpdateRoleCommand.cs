using AuthServer.Application.Mapping;
using AuthServer.Entities;
using AutoMapper;
using MediatR;
using System;

namespace AuthServer.Application.Roles.Commands
{
    public class UpdateRoleCommand : IRequest<bool>, IMappingProfile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public UpdateRoleCommand SetId(Guid id)
        {
            Id = id;
            return this;
        }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateRoleCommand, Role>();
        }
    }
}
