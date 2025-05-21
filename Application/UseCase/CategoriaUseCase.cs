using Application.ApplicationDTO;
using Application.IGateways;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities;

namespace Application.UseCase
{
    public class CategoriaUseCase : ICategoriaUseCase
    {
        private readonly ICategoriaGateway _gateway;        

        public CategoriaUseCase(ICategoriaGateway gateway)
        {
            _gateway = gateway;
        }

        public Task<List<Categoria>> ListarCategorias()
        {
            return _gateway.ListarCategorias();
        }

        public async Task<Categoria> ObterCategoriaPorNome(string nome)
        {
            var categoria = await _gateway.ObterCategoriaPorNome(nome);
            return categoria;
        }

        public async Task<bool> ValidarCategoria(string nome)
        {
            try
            {
                var categorias = await _gateway.ListarCategorias();
                return categorias.Any(x => x.Nome.Trim().Equals(nome.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível validar a categoria '{nome}'.", ex);
            }
        }

        public async Task SalvarCategoria(CategoriaDTO dto)
        {
            try
            {
                var existente = await _gateway.ObterCategoriaPorNome(dto.Nome);
                if (existente != null)
                    throw new DomainException($"A categoria '{dto.Nome}' já está cadastrada!");

                var novaCategoria = new Categoria(dto.Nome);
                await _gateway.SalvarCategoria(novaCategoria);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível salvar a categoria", ex);
            }
        }

        public async Task EditarCategoria(string nome, CategoriaDTO dto)
        {
            try
            {
                var existente = await _gateway.ObterCategoriaPorNome(dto.Nome);
                if (existente != null && !string.Equals(nome, dto.Nome, StringComparison.OrdinalIgnoreCase))
                    throw new DomainException($"A categoria '{dto.Nome}' já está cadastrada!");

                var categoriaAtualizada = new Categoria(dto.Nome);
                await _gateway.EditarCategoria(nome, categoriaAtualizada);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível editar a categoria '{nome}'.", ex);
            }
        }

        public async Task DeletarCategoria(string nome)
        {
            try
            {
                var existente = await _gateway.ObterCategoriaPorNome(nome);
                if (existente == null)
                    throw new DomainException($"A categoria '{nome}' não está cadastrada.");

                await _gateway.DeletarCategoria(nome);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível deletar a categoria '{nome}'.", ex);
            }
        }
    }
}
