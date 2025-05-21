using Application.ApplicationDTO;
using Domain.Entities;

namespace Application.IUseCase
{
    public interface IPedidoUseCase
    {

        Task<List<Pedido>> ListarPedidos();
        Task<List<Pedido>> ListarPedidosPorStatus(string status);
        Task<List<Pedido>> ListarPedidosEmAndamento();
        Task<Pedido> ObterPedidoPorNumero(string numPedido);
        Task<string> SalvarPedido(PedidoApplicationDTO pedidoDTO);
        Task AtualizarStatus(string status, string numPedido);
        Task<bool> ValidarStatusPedido(string status, string numPedido);


    }
}
