namespace Application.ApplicationDTO
{
    public class PedidoApplicationDTO
    {
        public PedidoApplicationDTO(string cpf, List<ProdutoApplicationDTO> produtos)
        {
            Cpf = cpf;
            Produtos = produtos;
        }

        public string Cpf { get; private set; }
        public List<ProdutoApplicationDTO> Produtos { get; private set; }
    }
}
