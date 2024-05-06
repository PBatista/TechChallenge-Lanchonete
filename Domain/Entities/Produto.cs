using Domain.Base;

namespace Domain.Entities
{
    public class Produto : IAggregateRoot
    {
        public Produto(int id, string nome, int categoria_id, decimal preco, string descricao) 
        {

            Id = id;
            Nome = nome;
            Categoria_id = categoria_id;
            Preco = preco;
            Descricao = descricao;
            // Imagens = imagens;
        }
               
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int Categoria_id { get ; private set; }
        public decimal Preco { get; private set; }
        public string Descricao { get; private set; }
        // public string Imagens { get; private set; }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome não pode estar vazio");
            AssertionConcern.AssertArgumentNotNull(Categoria_id, "A categoria não pode estar vazia");
            AssertionConcern.AssertArgumentNotNull(Preco, "O Preço não pode estar vazio");
            AssertionConcern.AssertArgumentNotNull(Descricao, "A descrição não pode estar vazia");
            // AssertionConcern.AssertArgumentNotNull(Imagens, "As imagens não pode estar vazia");
        }

    }
}
