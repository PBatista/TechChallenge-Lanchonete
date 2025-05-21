using Application.ApplicationDTO;
using Application.IPresenters;
using Application.IUseCase;

namespace Application.ControllerApp
{
    public class CategoriaAppController
    {
        private readonly ICategoriaUseCase _useCase;
        private readonly ICategoriaPresenter _presenter;

        public CategoriaAppController(ICategoriaUseCase useCase, ICategoriaPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<List<CategoriaDTO>> ListarCategorias()
        {
            var categorias = await _useCase.ListarCategorias();
            return _presenter.PresentMany(categorias);
        }

        public async Task<CategoriaDTO> ObterPorNome(string nome)
        {
            var categoria = await _useCase.ObterCategoriaPorNome(nome);
            return categoria == null ? null : _presenter.Present(categoria);
        }

        public async Task SalvarCategoria(CategoriaDTO dto)
        {
            await _useCase.SalvarCategoria(dto);
        }

        public async Task EditarCategoria(string nome, CategoriaDTO dto)
        {
            await _useCase.EditarCategoria(nome, dto);
        }

        public async Task DeletarCategoria(string nome)
        {
            await _useCase.DeletarCategoria(nome);
        }

    }
}
