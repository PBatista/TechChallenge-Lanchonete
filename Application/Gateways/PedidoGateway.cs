using Application.IGateways;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Gateways
{
    public class PedidoGateway : IPedidoGateway
    {
        private readonly IPedidoRepository _repository;

        public PedidoGateway(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Pedido>> ListarPedidos()
        {
            return await _repository.ListarPedidos();
        }

        public async Task<Pedido> ObterPedidoPorNumero(string numPedido)
        {
            return await _repository.ObterPedidoPorNumero(numPedido);
        }

        public async Task<string> SalvarPedido(Pedido pedido)
        {
            return await _repository.SalvarPedido(pedido);
        }

        public async Task AtualizarStatusPedido(string status, string numPedido)
        {
            await _repository.AtualizarStatusPedido(status, numPedido);
        }

        public async Task<List<Pedido>> ListarPedidosPorStatus(string status)
        {
            return await _repository.ListarPedidosPorStatus(status);
        }

        public async Task<List<Pedido>> ListarPedidosEmAndamento()
        {
            return await _repository.ListarPedidosEmAndamento();
        }
    }
}
