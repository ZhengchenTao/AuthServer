using AutoMapper;
using System;
using System.Linq;

namespace AuthServer.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var maps = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
              .Where(x => typeof(IMappingProfile).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
              .Select(x => (IMappingProfile)Activator.CreateInstance(x)).ToList();

            foreach (var map in maps)
            {
                map.CreateMappings(this);
            }
        }
    }
}
