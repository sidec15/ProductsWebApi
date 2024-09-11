using Microsoft.EntityFrameworkCore;
using ProductsWebApi;
using ProductsWebApi.Mappers;
using ProductsWebApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

// Add DbContext with PostgreSQL configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProductDbContext>(options =>
    options
    .UseNpgsql(connectionString)
    );

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ProductProfile)));

builder.Services.AddTransient<IStoreService, StoreService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
