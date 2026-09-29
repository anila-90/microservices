using platformservice.data;
using Microsoft.EntityFrameworkCore;
using platformservice.models;
using platformservice.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecific",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("testDb");
});
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<PlatformProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseCors("AllowSpecific");
app.MapControllers();

PrepDb.PrepareDb(app);

app.Run();


