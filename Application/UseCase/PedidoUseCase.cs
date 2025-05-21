using Application.ApplicationDTO;
using Application.IGateways;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Entities.Enum;


namespace Application.UseCase
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IClienteUseCase _clienteUseCase;
        private readonly IProdutoUseCase _produtoUseCase;

        readonly string NUMERO_DO_PEDIDO_VAZIO = string.Empty;

        public PedidoUseCase(IPedidoGateway pedidoGateway, IClienteUseCase clienteUseCase, IProdutoUseCase produtoUseCase)
        {
            _pedidoGateway = pedidoGateway;
            _clienteUseCase = clienteUseCase;
            _produtoUseCase = produtoUseCase;
        }

        public async Task<List<Pedido>> ListarPedidos()
        {
            try
            {
                return await _pedidoGateway.ListarPedidos();
            }
            catch (Exception ex)
            {
                throw new DomainException("Não foi possível listar os pedidos.", ex);
            }
        }

        public async Task<Pedido> ObterPedidoPorNumero(string numPedido)
        {
            try
            {
                return await _pedidoGateway.ObterPedidoPorNumero(numPedido);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível obter o pedido '{numPedido}'.", ex);
            }
        }

        public async Task<string> SalvarPedido(PedidoApplicationDTO pedidoDTO)
        {
            try
            {
                Cliente? cliente = null;

                if (!string.IsNullOrWhiteSpace(pedidoDTO.Cpf))
                {
                    cliente = await _clienteUseCase.ObterClientePorCpf(pedidoDTO.Cpf);
                }
                else if (string.IsNullOrWhiteSpace(pedidoDTO.Descricao))
                {
                    pedidoDTO.Descricao = "Obs: Cliente optou por não se identificar";
                }
                else
                {
                    pedidoDTO.Descricao += $"{Environment.NewLine}Obs: Cliente optou por não se identificar";
                }

                var produtos = await _produtoUseCase.ListarProdutos(pedidoDTO);
                double valorTotal = produtos.Sum(p => p.Preco);

                var pedido = new Pedido(
                    NUMERO_DO_PEDIDO_VAZIO,
                    produtos,
                    cliente,
                    valorTotal,
                    StatusPedidoEnum.AGUARDANDO_PAGAMENTO.GetDescription(),
                    pedidoDTO.Descricao,
                    DateTime.Now
                );

                return await _pedidoGateway.SalvarPedido(pedido);
            }
            catch (Exception ex)
            {
                throw new DomainException("Não foi possível salvar o pedido.", ex);
            }
        }

        public async Task AtualizarStatus(string status, string numPedido)
        {
            try
            {
                await _pedidoGateway.AtualizarStatusPedido(status, numPedido);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível atualizar o pedido '{numPedido}' para o status '{status}'.", ex);
            }
        }

        public async Task<List<Pedido>> ListarPedidosEmAndamento()
        {
            try
            {
                return await _pedidoGateway.ListarPedidosEmAndamento();
            }
            catch (Exception ex)
            {
                throw new DomainException("Não foi possível listar os pedidos em andamento.", ex);
            }
        }

        public async Task<List<Pedido>> ListarPedidosPorStatus(string status)
        {
            try
            {
                return await _pedidoGateway.ListarPedidosPorStatus(status);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível listar os pedidos com status '{status}'.", ex);
            }
        }

        public async Task<bool> ValidarStatusPedido(string status, string numPedido)
        {
            try
            {
                var statusValidos = Enum.GetValues(typeof(StatusPedidoEnum))
                    .Cast<StatusPedidoEnum>()
                    .Select(e => e.GetDescription());

                if (!statusValidos.Contains(status))
                    throw new DomainException($"O status '{status}' não é válido.");

                var pedido = await _pedidoGateway.ObterPedidoPorNumero(numPedido)
                    ?? throw new DomainException($"Pedido número '{numPedido}' não encontrado.");

                if (pedido.Status == StatusPedidoEnum.AGUARDANDO_PAGAMENTO.GetDescription()
                 || pedido.Status == StatusPedidoEnum.FINALIZADO.GetDescription())
                    throw new DomainException($"Pedido '{numPedido}' está com status '{pedido.Status}' e não pode ser atualizado.");

                if (pedido.Status == status)
                    throw new DomainException($"Pedido '{numPedido}' já está com o status '{status}'.");

                return true;
            }
            catch (Exception ex)
            {
                throw new DomainException($"Erro ao validar o status '{status}' para o pedido '{numPedido}'.", ex);
            }
        }

    }
}
