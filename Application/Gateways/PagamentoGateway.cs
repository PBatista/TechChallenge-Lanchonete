using Application.IGateways;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Gateways
{
    public class PagamentoGateway : IPagamentoGateway
    {
        private readonly IPagamentoRepository _repository;

        public PagamentoGateway(IPagamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task SalvarPagamento(Pagamento pagamento)
        {
            await _repository.SalvarPagamento(pagamento);
        }
    }
}
