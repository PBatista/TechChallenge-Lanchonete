using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IGateways
{
    public interface IPedidoGateway
    {
        Task<List<Pedido>> ListarPedidos();
        Task<Pedido> ObterPedidoPorNumero(string numPedido);
        Task<string> SalvarPedido(Pedido pedido);
        Task AtualizarStatusPedido(string status, string numPedido);
        Task<List<Pedido>> ListarPedidosPorStatus(string status);
        Task<List<Pedido>> ListarPedidosEmAndamento();
    }
}
