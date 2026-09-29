using AutoMapper;
namespace platformservice.data
{
    
    public class PlatformProfile:Profile
    {
        public PlatformProfile()
        {
            CreateMap<platformservice.models.platform, platformservice.viewmodels.PlatformResponse>();
            CreateMap<platformservice.viewmodels.PlatformRequest, platformservice.models.platform>();
        }
    }
}