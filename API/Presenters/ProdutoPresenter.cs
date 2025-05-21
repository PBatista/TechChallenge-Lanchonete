using Application.ApplicationDTO;
using Application.IPresenters;
using Domain.Entities;

namespace API.Presenters
{
    public class ProdutoPresenter : IProdutoPresenter
    {
        public List<ProdutoDTO> PresentMany(List<Produto> produtos)
        {
            return produtos.Select(Present).ToList();
        }

        public ProdutoDTO Present(Produto produto)
        {
            return new ProdutoDTO
            {
                Nome = produto.Nome,
                Categoria = produto.Categoria,
                Preco = produto.Preco,
                Imagens = produto.Imagens,    
                Descricao = produto.Descricao                
            };
        }
    }
}
