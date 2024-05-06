using Application.IUseCase;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCase
{
    public class ProdutoUseCase : IProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoUseCase(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }       

        public Task<IEnumerable<Produto>> ListarProdutos()
        {
            return _produtoRepository.ListarProdutos();
        }

        public Task<IEnumerable<Produto>> ObterProdutosPorCategoria(int categoria_id)
        {
            return _produtoRepository.ObterProdutosPorCategoria(categoria_id);
        }

        public async Task SalvarProduto(Produto produto)
        {
            await _produtoRepository.SalvarProduto(produto);
        }

        public async Task EditarProduto(Produto produto)
        {
            await _produtoRepository.EditarProduto(produto);
        }

        public async Task DeletarProduto(int id)
        {
            await _produtoRepository.DeletarProduto(id);
        }
    }
}
