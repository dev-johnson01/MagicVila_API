

using MagicVila_WebAPI;
using MagicVila_WebAPI.Data;
using MagicVila_WebAPI.Repository;
using MagicVila_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().
    WriteTo.File("Log/vilaLog.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();*/

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IVilaRepository, VilaRepository>();
builder.Services.AddScoped<IVilaNumberRepository,VilaNumberRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    });

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
