using Domain.Entities;
using Domain.Repositories;
using InfraMongoDb.DTO;
using InfraMongoDb.Mapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace InfraMongoDb.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoCollection<BsonDocument> _produtoCollection;

        public ProdutoRepository(IMongoDatabase database)
        {
            _produtoCollection = database.GetCollection<BsonDocument>("produtos");
        }

        public Task DeletarProduto(string nome)
        {           

            var filtro = Builders<BsonDocument>.Filter.Eq("Nome", nome);
            var resultado = _produtoCollection.DeleteOne(filtro);
            if (resultado.DeletedCount > 0)
            {
                return Task.FromResult(resultado);
            }
            else
            {
                return null;
            }            
        }

        public  Task EditarProduto(string nome, Produto produto)
        {
            var filtro = Builders<BsonDocument>.Filter.Eq("Nome", nome);
            var atualizacao = Builders<BsonDocument>.Update.Set("Nome", produto.Nome)
                                                           .Set("Categoria", produto.Categoria)
                                                           .Set("Preco", produto.Preco)
                                                           .Set("Descricao", produto.Descricao)
                                                           .Set("Imagens", produto.Imagens);

            var resultado = _produtoCollection.UpdateOne(filtro, atualizacao);
            if (resultado.ModifiedCount > 0)
            {
                return Task.FromResult(resultado);
            }
            else
            {
                return null;
            }
           
        }

        public async Task<List<Produto>> ListarProdutos()
        {           
            var produtosDocuments = _produtoCollection.Find(new BsonDocument()).ToList();
            var produtos = new List<Produto>();

            foreach (var document in produtosDocuments)
            {
                //document.Remove("_id");                          
                var produtoDTO = BsonSerializer.Deserialize<ProdutoDTO>(document);

                Produto produto = ProdutoMapper.MapToEntity(produtoDTO);
                produtos.Add(produto);
            }
                        
            return await Task.FromResult(produtos);
        }

        public async Task<List<Produto>> ObterProdutosPorCategoria(string categoria)
        {           

            var filtro = Builders<BsonDocument>.Filter.Eq("Categoria", categoria);
            var resultado = _produtoCollection.Find(filtro).ToList();
            var produtos = new List<Produto>();

            if (resultado != null)
            {
                foreach (var document in resultado)
                {
                    var produtoDTO = BsonSerializer.Deserialize<ProdutoDTO>(document);
                    Produto produto = ProdutoMapper.MapToEntity(produtoDTO);
                    produtos.Add(produto);
                }
                return await Task.FromResult(produtos);
            }
            else
            {
                return null;
            }
        }

        public async Task<Produto> ObterProdutosPorNome(string nome)
        {            
            var filtro = Builders<BsonDocument>.Filter.Eq("Nome", nome);
            var resultado = _produtoCollection.Find(filtro).FirstOrDefault();
            var produtos = new List<Produto>();

            if (resultado != null)
            {
                var produtoDTO = BsonSerializer.Deserialize<ProdutoDTO>(resultado);
                Produto produto = ProdutoMapper.MapToEntity(produtoDTO);
                produtos.Add(produto);
                return await Task.FromResult(produto);
            }
            else
            {
                return null;
            }
        }

        public Task SalvarProduto(Produto produto)
        {
            _produtoCollection.InsertOne(produto.ToBsonDocument());
            return Task.FromResult(produto);
        }
        
    }
}
