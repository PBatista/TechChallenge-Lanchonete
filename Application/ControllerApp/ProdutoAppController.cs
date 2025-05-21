using Application.ApplicationDTO;
using Application.IPresenters;
using Application.IUseCase;
using Application.UseCase;

namespace Application.ControllerApp
{
    public class ProdutoAppController
    {
        private readonly IProdutoUseCase _produtoUseCase;
        private readonly ICategoriaUseCase _categoriaUseCase;
        private readonly IProdutoPresenter _presenter;

        public ProdutoAppController(
            IProdutoUseCase produtoUseCase,
            ICategoriaUseCase categoriaUseCase,
            IProdutoPresenter presenter)
        {
            _produtoUseCase = produtoUseCase;
            _categoriaUseCase = categoriaUseCase;
            _presenter = presenter;
        }

        public async Task<List<ProdutoDTO>> ListarProdutos()
        {
            var produtos = await _produtoUseCase.ListarProdutos();
            return _presenter.PresentMany(produtos);
        }

        public async Task<ProdutoDTO> ObterProdutoPorNome(string nome)
        {
            var produto = await _produtoUseCase.ObterProdutoPorNome(nome);
            return produto == null ? null : _presenter.Present(produto);
        }

        public async Task<List<ProdutoDTO>> ObterProdutosPorCategoria(string categoria)
        {
            var produtos = await _produtoUseCase.ObterProdutosPorCategoria(categoria);
            return _presenter.PresentMany(produtos);
        }

        public async Task SalvarProduto(ProdutoDTO dto)
        {
            var produtoCadastrado = await _produtoUseCase.ObterProdutoPorNome(dto.Nome);
            if (produtoCadastrado != null)
                throw new Exception($"O produto '{dto.Nome}' já está cadastrado.");

            var categoriaValida = await _categoriaUseCase.ValidarCategoria(dto.Categoria);
            if (!categoriaValida)
                throw new Exception($"A categoria '{dto.Categoria}' não existe na base de dados.");

            await _produtoUseCase.SalvarProduto(dto);
        }

        public async Task EditarProduto(string nome, ProdutoDTO dto)
        {
            await _produtoUseCase.EditarProduto(nome, dto);
        }

        public async Task DeletarProduto(string nome)
        {
            await _produtoUseCase.DeletarProduto(nome);
        }

    }
}
