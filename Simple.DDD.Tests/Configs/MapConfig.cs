using AutoMapper;

namespace Simple.DDD.Tests.Configs
{
    public static class MapConfig
    {
        public static IMapper Get()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DDD.API.Profiles.MappingProfile());
            });

            return mockMapper.CreateMapper();
        }
    }
}
