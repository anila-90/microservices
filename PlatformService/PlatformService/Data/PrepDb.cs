using platformservice.data;

namespace platformservice.repository
{
    public static class PrepDb
    {
        public static void PrepareDb(IApplicationBuilder app)
        {
            using var scope= app.ApplicationServices.CreateScope();
            var context=scope.ServiceProvider.GetRequiredService<AppDbContext>();
            SeedData(context);

        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                new models.platform{Name="Dot Net",Publisher="Microsoft"},
                new models.platform{Name="SQL Server Express",Publisher="Microsoft"},
                new models.platform{Name="Kubernetes",Publisher="Cloud Native Computing Foundation"}
                );
                context.SaveChanges();
            }
        }

    }
}