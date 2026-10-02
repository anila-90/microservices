using platformservice.data;
using Microsoft.EntityFrameworkCore;
using platformservice.models;
using platformservice.repository;
using platformservice.middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var config = builder.Configuration;
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("rate-limit",options=>

    {
        options.PermitLimit= 10;
        options.Window= TimeSpan.FromSeconds(5);
    }
    );
});
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>

{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
      
        
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:key"])),
        ValidIssuer = config["jwt:issuer"],
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidAudience = config["jwt:audience"],
        ValidateAudience= true,
        ValidateLifetime = true
    };
})
;

builder.Services.AddAuthorization();
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

app.UseMiddleware<ExceptionHandlerMiddleware>();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecific");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

PrepDb.PrepareDb(app);

app.Run();


