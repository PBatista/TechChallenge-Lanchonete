using Application.ApplicationDTO;
using Application.IPresenters;
using Application.IUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ControllerApp
{
    public class PedidoAppController
    {
        private readonly IPedidoUseCase _useCase;
        private readonly IPedidoPresenter _presenter;

        public PedidoAppController(IPedidoUseCase useCase, IPedidoPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<string> SalvarPedido(PedidoApplicationDTO dto)
        {
            return await _useCase.SalvarPedido(dto);
        }

        public async Task<List<PedidoDTO>> ListarPedidos()
        {
            var pedidos = await _useCase.ListarPedidos();
            return _presenter.PresentMany(pedidos);
        }

        public async Task<PedidoDTO> ObterPedidoPorNumero(string numero)
        {
            var pedido = await _useCase.ObterPedidoPorNumero(numero);
            return pedido == null ? null : _presenter.Present(pedido);
        }

        public async Task AtualizarStatus(string status, string numero)
        {
            if (await _useCase.ValidarStatusPedido(status, numero))
            {
                await _useCase.AtualizarStatus(status, numero);
            }
        }

        public async Task<List<PedidoDTO>> ListarPedidosPorStatus(string status)
        {
            var pedidos = await _useCase.ListarPedidosPorStatus(status);
            return _presenter.PresentMany(pedidos);
        }

        public async Task<List<PedidoDTO>> ListarPedidosEmAndamento()
        {
            var pedidos = await _useCase.ListarPedidosEmAndamento();
            return _presenter.PresentMany(pedidos);
        }
    }
}
