using System;
using platformservice.models;
using platformservice.data;
namespace platformservice.repository
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;
        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool AddPlatform(platform platform)
        {
           _context.Platforms.Add(platform);
              return _context.SaveChanges() > 0;
        }

        public IEnumerable<platform> GetPlatforms()
        {
           return _context.Platforms.ToList();
        }
    }
}