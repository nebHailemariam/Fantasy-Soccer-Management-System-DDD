using System.Reflection;
using FantasySoccerPublic.Data;
using FantasySoccerPublic.Handlers;
using FantasySoccerPublic.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Configure database connection.
builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"),
            providerOptions => providerOptions.EnableRetryOnFailure(3)));

// Add repositories to Dependency injection container
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

// Add services to Dependency injection container.
builder.Services.AddScoped<IPlayerService, PlayerService>();

// Add hosted Services
builder.Services.AddHostedService<PlayerAddedEventListener>();
builder.Services.AddHostedService<TeamAddedEventListener>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
