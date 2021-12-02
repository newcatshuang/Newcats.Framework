using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Newcats.Framework.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JobManager Api",
        Description = "The Job Manager Api document",
        Contact = new OpenApiContact
        {
            Name = "Newcats",
            Email = string.Empty,
            Url = new Uri("https://www.newcats.xyz")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://www.newcats.xyz/license.html")
        }
    });

    c.SchemaFilter<AddEnumDescriptionFilter>();
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
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
