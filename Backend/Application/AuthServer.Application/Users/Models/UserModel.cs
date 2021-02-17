using AuthServer.Application.BaseModel;
using AuthServer.Application.Mapping;
using AuthServer.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AuthServer.Application.Users.Models
{
    public class UserModel : AuditModel, IMappingProfile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Guid? TenantId { get; set; }

        public bool IsDisabled { get; set; }

        public string TenantName { get; set; }

        public List<UserIdentityModel> UserIdentities { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserModel>()
                .ForMember(x => x.TenantName, opt => opt.MapFrom(x => x.Tenant.Name));
            configuration.CreateMap<UserModel, User>();
        }
    }
}
