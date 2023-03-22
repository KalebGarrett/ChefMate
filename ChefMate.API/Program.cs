using ChefMate.API.Repositories;
using ChefMate.API.Repositories.Interfaces;
using ChefMate.Models;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMongoRepository<Recipe>, RecipeRepository>();
var conventionPack = new  ConventionPack {new CamelCaseElementNameConvention()};
ConventionRegistry.Register("camelCase", conventionPack, t => true);
            
var mongoDbConnectionString = builder.Configuration["ConnectionStrings:Mongo"];
builder.Services.AddScoped<IMongoClient, MongoClient>(_ => new MongoClient(MongoClientSettings.FromConnectionString(mongoDbConnectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();