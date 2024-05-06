using Application.IUseCase;
using Application.UseCase;
using Domain.Repositories;
using Infra.Persistence;
using Infra.Repositories;
using Infra.Sql;
using Microsoft.EntityFrameworkCore;


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
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
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
