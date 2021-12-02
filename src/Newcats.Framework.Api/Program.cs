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
    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Newcats.Framework.Model.xml"));
    //TODO:Summary注释(xml文件)和AddEnumDescriptionFilter不能两全
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
