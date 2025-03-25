using Application.IUseCase;
using Application.UseCase;
using Domain.Repositories;
using InfraMongoDb.Repositories;
using MercadoPago.IService;
using MercadoPago.Service;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services && Repositories && DB Configurations 
builder.Services.AddTransient<IProdutoUseCase, ProdutoUseCase>();
builder.Services.AddTransient<IClienteUseCase, ClienteUseCase>();
builder.Services.AddTransient<ICategoriaUseCase, CategoriaUseCase>();
builder.Services.AddTransient<IPedidoUseCase, PedidoUseCase>();
builder.Services.AddTransient<ICheckoutUseCase, CheckoutUseCase>();
builder.Services.AddTransient<INotificaoUseCase, NotificaoUseCase>();

builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddTransient<IPagamentoRepository, PagamentoRepository>();

builder.Services.AddTransient<IMercadoPagoService, MercadoPagoService>();

// Registro do IMongoClient
//builder.Services.AddSingleton<IMongoClient>(sp =>
//{
//    var connectionString = Environment.GetEnvironmentVariable("MONGO_URI");
//    return new MongoClient(connectionString);
//});

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var mongoUser = Environment.GetEnvironmentVariable("MONGO_USERNAME");
    var mongoPass = Environment.GetEnvironmentVariable("MONGO_PASSWORD");
    var mongoUri = $"mongodb://{mongoUser}:{mongoPass}@mongo-service:27018/?authMechanism=SCRAM-SHA-256";
    // var mongoUri = "mongodb://127.0.0.1:27017/lanchonete";
    return new MongoClient(mongoUri);
});


// Registro do IMongoDatabase
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetService<IMongoClient>();
    var databaseName = "lanchonete"; // Nome do banco de dados
    return client.GetDatabase(databaseName);
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
