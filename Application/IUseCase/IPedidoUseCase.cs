using Application.ApplicationDTO;
using Domain.Entities;

namespace Application.IUseCase
{
    public interface IPedidoUseCase
    {       

        Task<string> SalvarPedido(PedidoApplicationDTO pedido);       
        Task<List<Pedido>> ListarPedidos();
        Task<List<Pedido>> ListarPedidosEmAndamento();
        Task<List<Pedido>> ListarPedidosPorStatus(string status);
        Task<Pedido> ObterPedidoPorNumero(string numPedido);
        Task AtualizarStatus(string status, string numPedido);
        Task<bool> ValidarStatusPedido(string numPedido, string status);


    }
}
