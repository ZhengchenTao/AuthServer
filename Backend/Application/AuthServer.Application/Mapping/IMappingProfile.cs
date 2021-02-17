using AutoMapper;

namespace AuthServer.Application.Mapping
{
    public interface IMappingProfile
    {
        void CreateMappings(Profile configuration);
    }
}
