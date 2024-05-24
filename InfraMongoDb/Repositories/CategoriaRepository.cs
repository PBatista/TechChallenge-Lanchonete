using Domain.Entities;
using Domain.Repositories;
using InfraMongoDb.DTO;
using InfraMongoDb.Mapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace InfraMongoDb.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IMongoCollection<BsonDocument> _categoriaCollection;

        public CategoriaRepository(IMongoDatabase database)
        {
            _categoriaCollection = database.GetCollection<BsonDocument>("categorias");
        }

        public async Task SalvarCategoria(Categoria categoria)
        {
            await _categoriaCollection.InsertOneAsync(categoria.ToBsonDocument());
        }

        public Task EditarCategoria(string nome, Categoria categoria)
        {
            var filtro = Builders<BsonDocument>.Filter.Eq("Nome", nome.ToUpper());
            var atualizacao = Builders<BsonDocument>.Update.Set("Nome", categoria.Nome);

            var resultado = _categoriaCollection.UpdateOne(filtro, atualizacao);
            if (resultado.ModifiedCount > 0)
            {
                return Task.FromResult(resultado);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Categoria>> ListarCategorias()
        {
            var categoriaDocuments = await _categoriaCollection.Find(new BsonDocument()).ToListAsync();
            var categorias = new List<Categoria>();

            foreach (var document in categoriaDocuments)
            {
                var categoriaDTO = BsonSerializer.Deserialize<CategoriaDTO>(document);
                Categoria categoria = CategoriaMapper.MapToEntity(categoriaDTO);
                categorias.Add(categoria);
            }

            return categorias;
        }

        public async Task<Categoria> ObterCategoriaPorNome(string nome)
        {
            var filtro = Builders<BsonDocument>.Filter.Eq("Nome", nome.ToUpper());
            var resultado = await _categoriaCollection.Find(filtro).FirstOrDefaultAsync();

            if (resultado != null)
            {
                var categoriaDTO = BsonSerializer.Deserialize<CategoriaDTO>(resultado);
                Categoria cliente = CategoriaMapper.MapToEntity(categoriaDTO);
                return cliente;
            }
            else return null;
        }

        public Task DeletarCategoria(string nome)
        {

            var filtro = Builders<BsonDocument>.Filter.Eq("Nome", nome.ToUpper());
            var resultado = _categoriaCollection.DeleteOne(filtro);
            if (resultado.DeletedCount > 0)
            {
                return Task.FromResult(resultado);
            }
            else
            {
                return null;
            }
        }

    }
}
