using Domain.Entities;
using Domain.Repositories;
using InfraMongoDb.DTO;
using InfraMongoDb.Mapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


namespace InfraMongoDb.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<BsonDocument> _pedidoCollection;

        public PedidoRepository(IMongoDatabase database)
        {
            _pedidoCollection = database.GetCollection<BsonDocument>("pedidos");
        }

        public async Task<string> SalvarPedido(Pedido pedido)
        {

            PedidoDTO pedidoDTO = PedidoMapper.MapToDTO(pedido);
            pedidoDTO.NumPedido = await ObterProximoNumeroPedidoAsync();
            _pedidoCollection.InsertOne(pedidoDTO.ToBsonDocument());

            return pedidoDTO.NumPedido;
        }              

        public async Task<List<Pedido>> ListarPedidos()
        {
            var pedidoDocuments = _pedidoCollection.Find(new BsonDocument()).ToList();
            var pedidos = new List<Pedido>();

            foreach (var document in pedidoDocuments)
            {
                // document.Remove("_id");                          
                var pedidoDTO = BsonSerializer.Deserialize<PedidoDTO>(document);

                Pedido pedido = PedidoMapper.MapToEntity(pedidoDTO);
                pedidos.Add(pedido);
            }

            return await Task.FromResult(pedidos);
        }

        public async Task<List<Pedido>> ListarPedidosEmAndamento()
        {
                   
            var filterFinalizado = Builders<BsonDocument>.Filter.Ne("Status", "Finalizado");
            var filterNull = Builders<BsonDocument>.Filter.Ne("Status", "");
            var combinedFilter = Builders<BsonDocument>.Filter.And(filterFinalizado, filterNull);

            var pedidoDocuments = await _pedidoCollection.Find(combinedFilter).ToListAsync();
            var pedidos = new List<Pedido>();

            foreach (var document in pedidoDocuments)
            {
                var pedidoDTO = BsonSerializer.Deserialize<PedidoDTO>(document);
                Pedido pedido = PedidoMapper.MapToEntity(pedidoDTO);
                pedidos.Add(pedido);
            }

            return pedidos;
        }

        public Task<Pedido> ObterPedidoPorNumero(string numPedido)
        {
            var filtro = Builders<BsonDocument>.Filter.Eq("NumPedido", numPedido);            
            var resultado = _pedidoCollection.Find(filtro).FirstOrDefault();

            if (resultado != null)
            {
                var pedidoDTO = BsonSerializer.Deserialize<PedidoDTO>(resultado);
                Pedido pedido = PedidoMapper.MapToEntity(pedidoDTO);
                return Task.FromResult(pedido);
            }
            else
            {
                return null;
            }
        }

        public Task AtualizarStatusPedido(string status, string numPedido)
        {            

            var filtro = Builders<BsonDocument>.Filter.Eq("NumPedido", numPedido);
            var atualizacao = Builders<BsonDocument>.Update.Set("Status", status);                                                      

            var resultado = _pedidoCollection.UpdateOne(filtro, atualizacao);

            if (resultado.ModifiedCount > 0)
            {
                return Task.FromResult(resultado);
            }
            else
            {
                return null;
            }
        }

        public async Task<string> ObterProximoNumeroPedidoAsync()
        {            

            var ultimoPedido = await _pedidoCollection
                .Find(new BsonDocument())
                .Sort(Builders<BsonDocument>.Sort.Descending("NumPedido"))
                .Limit(1)
                .ToListAsync();

            if (ultimoPedido.Any() && ultimoPedido[0].Contains("NumPedido"))
            {
                var ultimoNumPedido = ultimoPedido[0]["NumPedido"].AsString;
                int numeroPedido;
                if (int.TryParse(ultimoNumPedido, out numeroPedido))
                {
                    var proximoNumeroPedido = (++numeroPedido).ToString().PadLeft(3, '0');
                    return proximoNumeroPedido;
                }
            }

            return "001";
        }
        
    }
}
