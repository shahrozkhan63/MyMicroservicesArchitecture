using EventBus.Interfaces;
using EventBus.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using ProductService.Api.Handlers;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register RabbitMQ Event Bus
builder.Services.AddSingleton<IEventBus>(sp => new RabbitMQEventBus("amqp://guest:guest@localhost:5673"));

// Register the event handler
builder.Services.AddSingleton<OrderEventHandler>();
// Configure the web host to use specific URLs
builder.WebHost.UseUrls("http://localhost:8081"); //, "https://localhost:44363"
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
