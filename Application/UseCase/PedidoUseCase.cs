using Application.ApplicationDTO;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories;
using InfraMongoDb.Repositories;

namespace Application.UseCase
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoUseCase(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
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
                Cliente cliente;
                if (pedidoDTO.Cpf != null && pedidoDTO.Cpf.Trim() != "")
                {
                    cliente = await _clienteRepository.ObterClientePorCpf(pedidoDTO.Cpf);
                }
                else
                {
                    cliente = new Cliente("", "", "", "Cliente não se indentificou");
                }

                List<Produto> listaProdutos = new List<Produto>();

                foreach (var produto in pedidoDTO.Produtos)
                {
                    Produto prod = await _produtoRepository.ObterProdutosPorNome(produto.Nome);

                    if (prod == null)
                    {
                        throw new DomainException($"Produto '{produto.Nome}' não encontrado.");
                    }

                    listaProdutos.Add(prod);
                }

                double valorTotal = listaProdutos.Sum(x => x.Preco);

                // CPF pode ser nulo se o cliente não estiver identificado
                Pedido pedido = new Pedido("", listaProdutos, cliente, valorTotal, "", DateTime.Now);
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
    }
}
