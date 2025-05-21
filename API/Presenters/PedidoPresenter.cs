using Application.ApplicationDTO;
using Application.IPresenters;
using Domain.Entities;

namespace API.Presenters
{
    public class PedidoPresenter : IPedidoPresenter
    {
        public PedidoDTO Present(Pedido pedido)
        {
            return new PedidoDTO
            {
                NumPedido = pedido.NumPedido,
                Status = pedido.Status,                
                Descricao = pedido.Descricao,
                ValorTotal = pedido.ValorTotal,
                DataHora = pedido.DataHora,
                Produtos = pedido.Produtos.Select(p => new ProdutoDTO
                {
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Categoria = p.Categoria
                }).ToList()
            };
        }

        public List<PedidoDTO> PresentMany(List<Pedido> pedidos)
        {
            return pedidos.Select(Present).ToList();
        }
    }
}
