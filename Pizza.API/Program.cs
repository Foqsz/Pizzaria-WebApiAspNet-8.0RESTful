using Microsoft.EntityFrameworkCore;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs.Mappings;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Data;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Repository;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PizzariaContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(typeof(PizzaMappingProfile));

builder.Services.AddScoped<IPizzariaService, PizzariaService>();
builder.Services.AddScoped<IPizzariaRepository, PizzariaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzaria API V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
