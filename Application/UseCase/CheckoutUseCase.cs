using Application.IGateways;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities.Enum;
using Domain.Repositories;
using MercadoPago.IService;

namespace Application.UseCase
{
    public class CheckoutUseCase : ICheckoutUseCase
    {
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IPagamentoGateway _pagamentoGateway;
        private readonly IMercadoPagoService _mercadoPagoService;

        public CheckoutUseCase(
            IPedidoGateway pedidoGateway,
            IMercadoPagoService mercadoPagoService,
            IPagamentoGateway pagamentoGateway)
        {
            _pedidoGateway = pedidoGateway;
            _mercadoPagoService = mercadoPagoService;
            _pagamentoGateway = pagamentoGateway;
        }

        public async Task ProcessarPagamento(string numPedido)
        {
            try
            {
                var pedido = await _pedidoGateway.ObterPedidoPorNumero(numPedido)
                             ?? throw new DomainException("Pedido não foi encontrado!");

                if (pedido.Status != StatusPedidoEnum.AGUARDANDO_PAGAMENTO.GetDescription())
                    throw new DomainException("O pedido já está com o pagamento aprovado!");

                var pagamento = await _mercadoPagoService.FakePagamento(pedido);

                if (pagamento.StatusPagamento == "APROVADO")
                {
                    await _pagamentoGateway.SalvarPagamento(pagamento);
                    await _pedidoGateway.AtualizarStatusPedido(StatusPedidoEnum.RECEBIDO.GetDescription(), numPedido);
                }
                else
                {
                    throw new DomainException("O pagamento falhou. Pedido não finalizado.");
                }
            }
            catch (Exception ex)
            {
                throw new DomainException("O pagamento falhou. Pedido não finalizado.", ex);
            }
        }
    }
}