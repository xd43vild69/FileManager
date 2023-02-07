using Microsoft.Extensions.Configuration;
using Domain;

var builder = WebApplication.CreateBuilder(args);
//IConfiguration configuration = builder.Configuration;

// Add services to the container.

// IConfiguration config = new ConfigurationBuilder()
//     .AddJsonFile("appsettings.json")
//     .AddEnvironmentVariables()
//     .Build();

// var pathFiles = config.GetRequiredSection("PathFiles");

// Console.WriteLine($"Path to files = {pathFiles}");




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IImageManager, ImageManager>();



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
