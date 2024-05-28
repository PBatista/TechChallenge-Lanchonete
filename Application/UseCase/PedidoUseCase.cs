using Application.ApplicationDTO;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Repositories;


namespace Application.UseCase
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteUseCase _clienteUseCase;
        private readonly IProdutoUseCase _produtoUseCase;
        readonly string NUMERO_DO_PEDIDO_VAZIO = string.Empty;

        public PedidoUseCase(IPedidoRepository pedidoRepository, IClienteUseCase clienteUseCase, IProdutoUseCase produtoUseCase)
        {
            _pedidoRepository = pedidoRepository;
            _clienteUseCase = clienteUseCase;
            _produtoUseCase = produtoUseCase;
        }

        public async Task<List<Pedido>> ListarPedidos()
        {
            try
            {
                return await _pedidoRepository.ListarPedidos();
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível listar os pedidos.", ex);
            }
        }

        public async Task<Pedido> ObterPedidoPorNumero(string numPedido)
        {
            try
            {
                return await _pedidoRepository.ObterPedidoPorNumero(numPedido);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível obter o pedido com número '{numPedido}'.", ex);
            }

        }

        public async Task<string> SalvarPedido(PedidoApplicationDTO pedidoDTO)
        {
            try
            {
                Cliente? cliente = null;

                if (!string.IsNullOrWhiteSpace(pedidoDTO.Cpf))
                    cliente = await _clienteUseCase.ObterClientePorCpf(pedidoDTO.Cpf);
                else
                    pedidoDTO.Descricao = string.IsNullOrWhiteSpace(pedidoDTO.Descricao)
                        ? "Obs: Cliente optou por não se identificar"
                        : $"{pedidoDTO.Descricao}{Environment.NewLine}Obs: Cliente optou por não se identificar";

                List<Produto> listaProdutos = await _produtoUseCase.ListarProdutos(pedidoDTO);
                double valorTotal = listaProdutos.Sum(prod => prod.Preco);

                Pedido pedido = new(NUMERO_DO_PEDIDO_VAZIO, listaProdutos, cliente, valorTotal, StatusPedidoEnum.AGUARDANDO_PAGAMENTO.GetDescription(), pedidoDTO.Descricao, DateTime.Now);
                return await _pedidoRepository.SalvarPedido(pedido);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível salvar o pedido.", ex);
            }
        }

        public Task AtualizarStatus(string status, string numPedido)
        {
            try
            {
                return _pedidoRepository.AtualizarStatusPedido(status, numPedido);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível atualizar o pedido número '{numPedido}' com o status ${status}.", ex);
            }
        }

        public Task<List<Pedido>> ListarPedidosEmAndamento()
        {
            try
            {
                return _pedidoRepository.ListarPedidosEmAndamento();
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível listar os pedidos em andamento.", ex);
            }
        }

        public Task<List<Pedido>> ListarPedidosPorStatus(string status)
        {
            try
            {
                return _pedidoRepository.ListarPedidosPorStatus(status);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível listar os pedidos pelo status {status}.", ex);
            }
        }

        public async Task<bool> ValidarStatusPedido(string status, string numPedido)
        {
            try
            {
                var listaStatusValido = Enum.GetValues(typeof(StatusPedidoEnum))
                          .Cast<StatusPedidoEnum>()
                          .Select(e => e.GetDescription());

                if (!listaStatusValido.Contains(status))
                    throw new DomainException($"O status '{status}' não é válido para o pedido número '{numPedido}'.");

                var pedido = await _pedidoRepository.ObterPedidoPorNumero(numPedido) ?? throw new DomainException($"Não foi possível encontrar o pedido número '{numPedido}'.");

                if (pedido.Status == StatusPedidoEnum.AGUARDANDO_PAGAMENTO.GetDescription() || pedido.Status == StatusPedidoEnum.FINALIZADO.GetDescription())
                    throw new DomainException($"Não foi possível atualizar o pedido número '{numPedido}' com o status '{status}', pois o pedido está com o status '{pedido.Status}'.");

                if (pedido.Status == status)
                    throw new DomainException($"Não foi possível atualizar o pedido número '{numPedido}' com o status '{status}', pois o status já está como '{pedido.Status}'.");

                return true;
            }
            catch (Exception ex)
            {
                throw new DomainException($"O status '{status}' não é válido para o pedido número '{numPedido}'.", ex);
            }
        }

    }
}
