using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Repositories;
using Hypesoft.Application.Handlers.Produtos;
using MediatR;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("MongoDb"));
var database = mongoClient.GetDatabase("HypesoftDb"); 

builder.Services.AddSingleton(database); 
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddMediatR(typeof(CreateProdutoHandler).Assembly);

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();