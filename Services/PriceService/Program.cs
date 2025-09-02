using Microsoft.EntityFrameworkCore;
using PriceService.Data;
using PriceService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PriceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPriceService, PriceService.Services.PriceService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
