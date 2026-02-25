using Hypesoft.Infrastructure.Configurations;
using Hypesoft.Application.Handlers.Produtos;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

var mongoConnection = builder.Configuration.GetConnectionString("MongoDb");
if (mongoConnection == null)
{
    throw new InvalidOperationException("Connection string 'MongoDb' is missing.");
}

builder.Services.AddInfrastructure(mongoConnection, "HypesoftDb");

// Registra o MediatR para os handlers
builder.Services.AddMediatR(typeof(CreateProdutoHandler).Assembly);

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Map Controllers
app.MapControllers();

app.Run();