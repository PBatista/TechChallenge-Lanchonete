using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories;
using MercadoPago.IService;

namespace Application.UseCase
{
    public class CheckoutUseCase : ICheckoutUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMercadoPagoService _mercadoPagoService;

        public CheckoutUseCase(IPedidoRepository pedidoRepository, IMercadoPagoService mercadoPagoService)
        {
            _pedidoRepository = pedidoRepository;
            _mercadoPagoService = mercadoPagoService;
        }

        public async Task ProcessarPagamento(string numPedido)
        {
            try
            {
                Pedido pedido = await _pedidoRepository.ObterPedidoPorNumero(numPedido);

                // Verifica se o pedido já foi finalizado
                if (pedido.Status == "Finalizado")
                {
                    Console.WriteLine("O pedido já foi finalizado.");
                    return;
                }

                // Realiza o pagamento
                var pagamento = await _mercadoPagoService.FakePagamento(pedido);

                // Verifica se o pagamento foi bem-sucedido
                if (pagamento.StatusPagamento == "Aprovado")
                {
                    var resultadoStatus = _pedidoRepository.AtualizarStatusPedido("Recebido", numPedido);

                    Console.WriteLine("Pedido finalizado com sucesso após o pagamento.");
                }
                else
                {
                    Console.WriteLine("O pagamento falhou. Pedido não finalizado.");
                }
            }
            catch (Exception ex)
            {
                throw new DomainException($"O pagamento falhou. Pedido não finalizado.", ex);
            }            
        }
    }
}
