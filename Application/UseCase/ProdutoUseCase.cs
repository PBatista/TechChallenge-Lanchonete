using Application.ApplicationDTO;
using Application.IGateways;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities;

namespace Application.UseCase
{
    public class ProdutoUseCase : IProdutoUseCase
    {
        private readonly IProdutoGateway _gateway;

        public ProdutoUseCase(IProdutoGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            try
            {
                return await _gateway.ListarProdutos();
            }
            catch (Exception ex)
            {
                throw new DomainException("Não foi possível listar os produtos.", ex);
            }
        }

        public async Task<List<Produto>> ObterProdutosPorCategoria(string categoria)
        {
            try
            {
                return await _gateway.ObterProdutosPorCategoria(categoria.Trim());
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível obter os produtos pela categoria '{categoria.Trim()}'.", ex);
            }
        }

        public async Task<Produto> ObterProdutoPorNome(string nome)
        {
            try
            {
                return await _gateway.ObterProdutoPorNome(nome.Trim());
            }
            catch (Exception ex)
            {
                throw new DomainException("Não foi possível obter o produto.", ex);
            }
        }

        public async Task<List<Produto>> ListarProdutos(PedidoApplicationDTO pedidoDTO)
        {
            try
            {
                List<Produto> listaProdutos = [];

                foreach (var produtoDTO in pedidoDTO.Produtos)
                {
                    var produto = await _gateway.ObterProdutoPorNome(produtoDTO.Nome)
                        ?? throw new DomainException($"Produto '{produtoDTO.Nome}' não encontrado.");

                    var produtosRepetidos = Enumerable.Range(0, produtoDTO.Quantidade).Select(_ => produto);
                    listaProdutos.AddRange(produtosRepetidos);
                }

                return listaProdutos;
            }
            catch (Exception ex)
            {
                throw new DomainException("Não foi possível obter os produtos para o pedido.", ex);
            }
        }

        public async Task SalvarProduto(ProdutoDTO dto)
        {
            try
            {
                var novoProduto = new Produto(dto.Nome, dto.Categoria, dto.Preco, dto.Descricao, dto.Imagens);
                await _gateway.SalvarProduto(novoProduto);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível salvar o produto '{dto.Nome}'.", ex);
            }
        }

        public async Task EditarProduto(string nome, ProdutoDTO dto)
        {
            try
            {
                var atualizado = new Produto(dto.Nome, dto.Categoria, dto.Preco, dto.Descricao, dto.Imagens);
                await _gateway.EditarProduto(nome, atualizado);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível editar o produto '{nome}'.", ex);
            }
        }

        public async Task DeletarProduto(string nome)
        {
            try
            {
                await _gateway.DeletarProduto(nome);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível deletar o produto '{nome}'.", ex);
            }
        }


    }
}
