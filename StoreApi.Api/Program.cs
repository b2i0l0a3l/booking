using BookingSystem.Infrastructure.presistence;
using ChatApi.Api.Extension;
using ChatApi.Application.ServiceRegistration;
using ChatApi.Infrastructure.Identity;
using ChatApi.Infrastructure.InfrastructureRegistration;
using ChatApi.Infrastructure.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer(); // مهم جدًا

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerConfig();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddInfrastructureRegistration();
builder.Services.AddApplicationRegistration();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // ينشئ swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatApi API V1");
        c.RoutePrefix = ""; // لو تريد واجهة Swagger تظهر على / مباشرة
    });

}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();
