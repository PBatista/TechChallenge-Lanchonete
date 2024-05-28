using Application.IUseCase;
using Domain.Base;
using Domain.Entities.Enum;
using Domain.Repositories;
using MercadoPago.IService;

namespace Application.UseCase
{
    public class CheckoutUseCase : ICheckoutUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMercadoPagoService _mercadoPagoService;

        public CheckoutUseCase(IPedidoRepository pedidoRepository, IMercadoPagoService mercadoPagoService, IPagamentoRepository pagamentoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _mercadoPagoService = mercadoPagoService;
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task ProcessarPagamento(string numPedido)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPedidoPorNumero(numPedido) ?? throw new DomainException($"Pedido não foi encontrado!");

                // Verifica se o status do pedido é Diferente de Recebido, caso for não pode alterar
                if (pedido.Status.Equals(StatusPedidoEnum.AGUARDANDO_PAGAMENTO.GetDescription()))
                {
                    // Realiza o pagamento
                    var pagamento = await _mercadoPagoService.FakePagamento(pedido);

                    // Verifica se o pagamento foi bem-sucedido
                    if (pagamento.StatusPagamento == "APROVADO")
                    {
                        await _pagamentoRepository.SalvarPagamento(pagamento);
                        var resultadoStatus = _pedidoRepository.AtualizarStatusPedido(StatusPedidoEnum.RECEBIDO.GetDescription(), numPedido);
                    }
                    else
                    {
                        throw new DomainException($"O pagamento falhou. Pedido não finalizado.");
                    }                                    
                }
                else throw new DomainException($"O pedido já está com o pagamento Aprovado!");

            }
            catch (Exception ex)
            {
                throw new DomainException($"O pagamento falhou. Pedido não finalizado.", ex);
            }            
        }
    }
}
