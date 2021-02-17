using AuthServer.Application.BaseModel;
using AuthServer.Application.Mapping;
using AuthServer.Entities;
using AutoMapper;
using System;

namespace AuthServer.Application.Roles.Models
{
    public class RoleModel : AuditModel, IMappingProfile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Role, RoleModel>();
        }
    }
}
