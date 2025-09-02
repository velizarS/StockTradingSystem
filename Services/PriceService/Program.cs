using Microsoft.EntityFrameworkCore;
using PriceService.Data;
using PriceService.Services;

var builder = WebApplication.CreateBuilder(args);

// ��������� �� DbContext � PostgreSQL
builder.Services.AddDbContext<PriceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ����������� �� �������
builder.Services.AddScoped<IPriceService, PriceService.Services.PriceService>();

// Swagger / OpenAPI ���� Swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �������� �� ����������
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
