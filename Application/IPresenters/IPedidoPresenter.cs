using Application.ApplicationDTO;
using Domain.Entities;

namespace Application.IPresenters
{
    public interface IPedidoPresenter
    {
        PedidoDTO Present(Pedido pedido);
        List<PedidoDTO> PresentMany(List<Pedido> pedidos);
    }
}
