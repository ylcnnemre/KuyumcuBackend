using System.Text;
using FluentValidation.AspNetCore;
using KuyumcuWebApi.Context;
using KuyumcuWebApi.dto;
using KuyumcuWebApi.middeware;
using KuyumcuWebApi.Repository;
using KuyumcuWebApi.Rules;
using KuyumcuWebApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().
AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore).AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<UserLoginDto>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuthService, AuthService>();
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<RegisterRules, RegisterRules>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "ab",
        ValidAudience = "ab",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["Token:SecurityKey"]
        )),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("database"));
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        await dbContext.Database.CanConnectAsync();
        var migrateElement = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        migrateElement.Database.Migrate();
        Console.WriteLine("Database connection successful.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<CustomAuthorizationMiddleware>();
/* app.UseMiddleware<CustomAuthorizationMiddleware>(); */

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
