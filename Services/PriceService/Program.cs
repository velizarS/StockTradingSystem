using Microsoft.EntityFrameworkCore;
using PriceService.Data;
using PriceService.Services;

var builder = WebApplication.CreateBuilder(args);

// Настройка на DbContext с PostgreSQL
builder.Services.AddDbContext<PriceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация на сервиса
builder.Services.AddScoped<IPriceService, PriceService.Services.PriceService>();

// Swagger / OpenAPI чрез Swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавяне на контролери
builder.Services.AddControllers();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
