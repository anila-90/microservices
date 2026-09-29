using System;
using platformservice.models;
namespace platformservice.repository
{
    public interface IPlatformRepo
    {
        IEnumerable<platform> GetPlatforms();

        public bool AddPlatform(platform platform);
       
    }
}