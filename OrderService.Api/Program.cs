using EventBus.Interfaces;
using EventBus.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.RabbitMQ;
using OrderService.Infrastructure.Repositories; // Ensure this is included for OrderDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Register RabbitMQ
//builder.Services.AddSingleton(new EventBusRabbitMQ(builder.Configuration["RabbitMqUri"]));
builder.Services.AddSingleton<IEventBus>(sp => new RabbitMQEventBus("amqp://guest:guest@localhost:5672"));

// Configure the web host to use specific URLs
builder.WebHost.UseUrls("http://localhost:5001", "https://localhost:44362");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
