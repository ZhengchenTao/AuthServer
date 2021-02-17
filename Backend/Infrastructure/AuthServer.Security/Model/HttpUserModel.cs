using AuthServer.Application.Mapping;
using AuthServer.Application.Users.Models;
using AuthServer.Enums;
using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthServer.Security.Model
{
    public class HttpUserModel : IMappingProfile
    {
        /// <summary>
        /// 根据HttpContext生成当前登陆用户信息
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static HttpUserModel GenerateHttpUserModel(IHttpContextAccessor httpContextAccessor)
        {
            var httpUser = httpContextAccessor.HttpContext.User;
            var providerString = httpUser.Claims.FirstOrDefault(x => x.Type == ExtendedClaimTypes.IdentityProvider)?.Value;

            return new HttpUserModel()
            {
                Identity = httpUser.Claims.FirstOrDefault(x => x.Type == ExtendedClaimTypes.ProviderUserId).Value,
                Provider = (IdentityProvider)Enum.Parse(typeof(IdentityProvider), providerString),
                Name = httpUser.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value,
                Email = httpUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                Phone = httpUser.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.PhoneNumber)?.Value,
            };
        }

        public string Identity { get; set; }

        public IdentityProvider Provider { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<HttpUserModel, UserModel>()
                .ForMember(x => x.UserIdentities, o => o.MapFrom(x => new List<UserIdentityModel>()
                {
                    new UserIdentityModel()
                    {
                        Identity = x.Identity,
                        Provider = x.Provider
                    }
                }));
        }
    }
}
