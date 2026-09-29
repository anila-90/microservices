using System;
using Microsoft.EntityFrameworkCore;
using platformservice.models;

namespace platformservice.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<platform> Platforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<platform>(entity=>
          {
             entity.HasKey(e=>e.Id);
             entity.Property(e=> e.Id).ValueGeneratedOnAdd();
             entity.Property(e=>e.Name).IsRequired();
          });
        }
    }
 
}