using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPedidoRepository
    {
        Task<string> SalvarPedido(Pedido produto);        
        Task AtualizarStatusPedido(string status, string numPedido);
        Task<List<Pedido>> ListarPedidos();
        Task<Pedido> ObterPedidoPorNumero(string numPedido);
        Task<List<Pedido>> ListarPedidosEmAndamento();
    }
}
