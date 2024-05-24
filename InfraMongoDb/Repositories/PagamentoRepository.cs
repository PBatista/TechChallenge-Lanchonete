using Domain.Entities;
using Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraMongoDb.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly IMongoCollection<BsonDocument> _pagamentoCollection;

        public PagamentoRepository(IMongoDatabase database)
        {
            _pagamentoCollection = database.GetCollection<BsonDocument>("pagamentos");
        }

        public async Task SalvarPagamento(Pagamento pagamento)
        {
            await _pagamentoCollection.InsertOneAsync(pagamento.ToBsonDocument());
        }
    }
}
