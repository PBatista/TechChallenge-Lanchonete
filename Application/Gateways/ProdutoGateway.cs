using Application.IGateways;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Gateways
{
    public class ProdutoGateway : IProdutoGateway
    {
        private readonly IProdutoRepository _repository;

        public ProdutoGateway(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            return await _repository.ListarProdutos();
        }

        public async Task<List<Produto>> ObterProdutosPorCategoria(string categoria)
        {
            return await _repository.ObterProdutosPorCategoria(categoria);
        }

        public async Task<Produto> ObterProdutoPorNome(string nome)
        {
            return await _repository.ObterProdutosPorNome(nome);
        }

        public async Task SalvarProduto(Produto produto)
        {
            await _repository.SalvarProduto(produto);
        }

        public async Task EditarProduto(string nome, Produto produto)
        {
            await _repository.EditarProduto(nome, produto);
        }

        public async Task DeletarProduto(string nome)
        {
            await _repository.DeletarProduto(nome);
        }
    }
}
