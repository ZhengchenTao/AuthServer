using AuthServer.Application.Mapping;
using AuthServer.Entities;
using AuthServer.Enums;
using AutoMapper;
using System;

namespace AuthServer.Application.Users.Models
{
    public class UserIdentityModel : IMappingProfile
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Identity { get; set; }

        public IdentityProvider Provider { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UserIdentityModel, UserIdentity>();
            configuration.CreateMap<UserIdentity, UserIdentityModel>();
        }
    }
}
