using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Services;
using OrderService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// DbContext � PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// �����������
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// ������
builder.Services.AddScoped<IOrderService, OrderService.Services.OrderService>();

// Swagger / OpenAPI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger ���� � Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
