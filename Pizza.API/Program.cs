using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs.Mappings;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Data;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Repository;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiPizzaria", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PizzariaContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
builder.Services.AddIdentity<ApplicationUserModel, IdentityRole>().AddEntityFrameworkStores<PizzariaContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(PizzaMappingProfile));

builder.Services.AddScoped<IPizzariaService, PizzariaService>();
builder.Services.AddScoped<IPizzariaRepository, PizzariaRepository>();
builder.Services.AddScoped<IPizzaCategoriaService, PizzaCategoriaService>();
builder.Services.AddScoped<IPizzaCategoriaRepository, PizzaCategoriaRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzaria API V1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
