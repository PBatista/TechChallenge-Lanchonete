using Domain.Entities;
using MercadoPago.Resource.Payment;

namespace MercadoPago.IService
{
    public interface IMercadoPagoService
    {
        Task<Pagamento> FakePagamento(Pedido pedido);
        Task<Payment> CriarPagamento(string descricao, double valor);

    }
}
