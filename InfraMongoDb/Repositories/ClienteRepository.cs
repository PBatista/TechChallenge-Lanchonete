using Domain.Entities;
using Domain.Repositories;
using InfraMongoDb.DTO;
using InfraMongoDb.Mapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


namespace InfraMongoDb.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IMongoCollection<BsonDocument> _clienteCollection;

        public ClienteRepository(IMongoDatabase database)
        {
            _clienteCollection = database.GetCollection<BsonDocument>("clientes");
        }

        public async Task<List<Cliente>> ListarCliente()
        {
            var clienteDocuments = await _clienteCollection.Find(new BsonDocument()).ToListAsync();
            var clientes = new List<Cliente>();

            foreach (var document in clienteDocuments)
            {
                var clienteDTO = BsonSerializer.Deserialize<ClienteDTO>(document);
                Cliente cliente = ClienteMapper.MapToEntity(clienteDTO);
                clientes.Add(cliente);
            }

            return clientes;
        }

        public async Task<Cliente> ObterClientePorCpf(string cpf)
        {            
            var filtro = Builders<BsonDocument>.Filter.Eq("Cpf", cpf);
            var resultado = await _clienteCollection.Find(filtro).FirstOrDefaultAsync();

            if (resultado != null)
            {
                var clienteDTO = BsonSerializer.Deserialize<ClienteDTO>(resultado);
                Cliente cliente = ClienteMapper.MapToEntity(clienteDTO);
                return cliente;
            }
            else
            {
                return new Cliente("", "", "", "");
            }
        }

        public async Task SalvarCliente(Cliente cliente)
        {
            await _clienteCollection.InsertOneAsync(cliente.ToBsonDocument());
        }
    }
}
