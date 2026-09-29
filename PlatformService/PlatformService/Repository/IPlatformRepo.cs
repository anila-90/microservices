using System;
using platformservice.models;
namespace platformservice.repository
{
    public interface IPlatformRepo
    {
        IEnumerable<platform> GetPlatforms();

        Task<platform> GetByIdAsync(int id);

        public bool AddPlatform(platform platform);
       
    }
}