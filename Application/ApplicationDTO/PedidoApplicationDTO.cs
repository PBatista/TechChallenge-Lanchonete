using Domain.Entities;
using InfraMongoDb.DTO;

namespace Application.ApplicationDTO
{
    public class PedidoApplicationDTO
    {
        public PedidoApplicationDTO(string cpf, List<ProdutoApplicationDTO> produtos, string descricao)
        {
            ValidateDTO(produtos);
            Cpf = cpf;
            Produtos = produtos;
            Descricao = descricao;
        }

        public string Cpf { get; private set; }
        public List<ProdutoApplicationDTO> Produtos { get; private set; }
        public string Descricao { get; set; }


        public static void ValidateDTO(List<ProdutoApplicationDTO> produtos)
        {
            if (produtos == null || produtos.Count == 0)
            {
                throw new ArgumentException("A lista de produtos não pode ser nula ou vazia.", nameof(produtos));
            }

            foreach (var produto in produtos)
            {
                if(string.IsNullOrWhiteSpace(produto.Nome))
                    throw new ArgumentException("O nome do produto não pode ser vazio!", nameof(produto));                
                if (produto.Quantidade < 1)                
                    throw new ArgumentException("A quantidade de cada produto deve ser maior ou igual a 1.", nameof(produtos));                
            }
        }       
    }
}
