using Microsoft.Extensions.Configuration;
using Domain;
using Repositories;
using DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
// Duplicate here any configuration sources you use.
configurationBuilder.AddJsonFile("AppSettings.json");
IConfiguration configuration = configurationBuilder.Build();

builder.Services.AddScoped<IImageManager, ImageManager>();
//builder.Services.AddScoped<IRepository<Image>, RepositoryImages<Image>>(); 
builder.Services.AddScoped<AbstractRepository> (_ => new RepositoryImages(configuration));

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
