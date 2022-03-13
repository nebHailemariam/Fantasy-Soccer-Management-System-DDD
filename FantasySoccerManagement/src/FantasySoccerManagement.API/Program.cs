using Autofac;
using Autofac.Extensions.DependencyInjection;
using FantasySoccerManagement.Infrastructure;
using FantasySoccerManagement.Infrastructure.Constants;
using FantasySoccerManagement.Infrastructure.Data;
using FantasySoccerManagement.Infrastructure.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
.AddJsonOptions(o =>
{
    // Map enum values to strings.
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // Ignore circular references.
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.WriteIndented = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Register the Swagger generator, defining 1 or more Swagger documents.
builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo
     {
         Version = "v1",
         Title = "Fantasy Soccer Management System API",
         Description = "A Fantasy Soccer Management System built using ASP.NET Core Web API",
         Contact = new OpenApiContact
         {
             Name = "Nebiyou Daniel Hailemariam",
             Email = "nebhailemariam@gmail.com",
             Url = new Uri("https://nebhailemariam.yolasite.com/"),
         },
         License = new OpenApiLicense
         {
             Name = "MIT License",
             Url = new Uri("https://opensource.org/licenses/MIT"),
         }
     });
     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
     {
         Name = "Authorization",
         Type = SecuritySchemeType.ApiKey,
         Scheme = "Bearer",
         BearerFormat = "JWT",
         In = ParameterLocation.Header,
         Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
     });
     c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
 });

// Configure database connection.
builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"),
            providerOptions => providerOptions.EnableRetryOnFailure(3)));

// Configure ASP.NET Core Identity.
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

// Configure JWT settings.
var jwtSettings = builder.Configuration.GetSection("JWTSettings");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
        ValidAudience = jwtSettings.GetSection("validAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

// Configure RabbitMQ
builder.Services.AddSingleton(serviceProvider =>
{
    var uri = new Uri("amqp://guest:guest@localhost:5672/");
    return new ConnectionFactory
    {
        Uri = uri,
        DispatchConsumersAsync = true
    };
});

// Enable SignalR
builder.Services.AddSignalR();

// Enabling Memory Caching
builder.Services.AddMemoryCache();

// Add to Dependency injection container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new DefaultInfrastructureModule(true)));

// Add CORS policy 
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure policies.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyConstants.IS_LEAGUE_MANAGER, policy => policy.RequireRole(RoleConstants.LEAGUE_MANAGER_ROLE));
    options.AddPolicy(PolicyConstants.IS_TEAM_MANAGER, policy => policy.RequireRole(RoleConstants.TEAM_MANAGER_ROLE));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace API
{
    public partial class Program
    {

    }
}
