using Hypesoft.Infrastructure.Configurations;
using Hypesoft.Application.Handlers.Produtos;
using Hypesoft.API.Middlewares;
using MediatR;
using Hypesoft.Infrastructure.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

var mongoConnection = builder.Configuration.GetConnectionString("MongoDb");
if (mongoConnection == null)
{
    throw new InvalidOperationException("Connection string 'MongoDb' is missing.");
}

builder.Services.AddInfrastructure(mongoConnection, "HypesoftDb");

// Registra o MediatR para os handlers
builder.Services.AddMediatR(typeof(CreateProdutoHandler).Assembly);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Hypesoft API",
        Version = "v1",
        Description = "API for product management"
    });
});

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hypesoft API v1");
});

app.UseMiddleware<ExceptionMiddleware>();
// Map Controllers
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<MongoSeeder>();
    await seeder.SeedAsync();
}

app.Run();