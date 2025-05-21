using Domain.Entities;

namespace Application.IGateways
{
    public interface IPagamentoGateway
    {
        Task SalvarPagamento(Pagamento pagamento);
    }
}
