using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPagamentoRepository
    {
        Task SalvarPagamento(Pagamento pagamento);
    }
}
