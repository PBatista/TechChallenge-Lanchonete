using Application.IUseCase;
using Application.UseCase;
using Domain.Repositories;
using Infra.Sql;
using InfraMongoDb.Repositories;
using MercadoPago.IService;
using MercadoPago.Service;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDbConfiguration.Configure();
builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer("Server=.\\SQLEXPRESS;Database=Lanchonete;Integrated Security=SSPI;TrustServerCertificate=True"));

#region Services && Repositories
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

builder.Services.AddTransient<IMercadoPagoService, MercadoPagoService>();

// Registro do IMongoClient
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = "mongodb://127.0.0.1:27017";
    return new MongoClient(connectionString);
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
