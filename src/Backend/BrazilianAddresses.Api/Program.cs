using System.Text.Json.Serialization;
using BrazilianAddresses.Api.Filters;
using BrazilianAddresses.Application;
using BrazilianAddresses.Infrastructure;
using BrazilianAddresses.Domain.Extension;
using BrazilianAddresses.Infrastructure.Migrations;
using BrazilianAddresses.Application.Services.Automapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API Endereços Brasileiros", Version = "1.0", Description = "Web API para gerenciamento de endereços brasileiros." });
    option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header utilizando o Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });
    option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            System.Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "SwaggerPolicy", policy =>
    {
        policy.WithOrigins("https://editor.swagger.io/").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddRepository(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperConfig());
}).CreateMapper());

builder.Services.AddScoped<UserAuthorizationAttribute>();
builder.Services.AddScoped<AdminAuthorizationAttribute>();

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

app.UseCors("SwaggerPolicy");

UpdateDatabase();

app.Run();

void UpdateDatabase()
{
    var connection = builder.Configuration.GetConnection();
    var databaseName = builder.Configuration.GetDatabaseName();

    Database.CreateDatabase(connection, databaseName);

    app.MigrateDatabase();
}