using Application.IGateways;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Gateways
{
    public class CategoriaGateway : ICategoriaGateway
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaGateway(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Categoria>> ListarCategorias()
        {
            return _repository.ListarCategorias();
        }

        public Task<Categoria> ObterCategoriaPorNome(string nome)
        {
            return _repository.ObterCategoriaPorNome(nome);
        }

        public async Task<bool> CategoriaExiste(string nome)
        {
            var existente = await _repository.ObterCategoriaPorNome(nome);
            return existente != null;
        }

        public Task SalvarCategoria(Categoria categoria)
        {
            return _repository.SalvarCategoria(categoria);
        }

        public Task EditarCategoria(string nome, Categoria categoria)
        {
            return _repository.EditarCategoria(nome, categoria);
        }

        public Task DeletarCategoria(string nome)
        {
            return _repository.DeletarCategoria(nome);
        }

    }
}
