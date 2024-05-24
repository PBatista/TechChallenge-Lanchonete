

using MongoDB.Bson;

namespace InfraMongoDb.DTO
{
    public class ProdutoDTO : BaseDTO
    {
        public ProdutoDTO(ObjectId _id, string nome, string categoria, double preco, string descricao, string imagens)
        {

            Id = _id;
            Nome = nome;
            Categoria = categoria.ToUpper();
            Preco = preco;
            Descricao = descricao;
            Imagens = imagens;
        }
        
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public double Preco { get; private set; }
        public string Descricao { get; private set; }
        public string Imagens { get; private set; }
    }
}
