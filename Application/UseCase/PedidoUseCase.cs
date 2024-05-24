using Application.ApplicationDTO;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories;
using InfraMongoDb.DTO;
using InfraMongoDb.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace Application.UseCase
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoUseCase(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository, ICategoriaUseCase categoriaUseCase)
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
                Cliente? cliente = null;
                if (pedidoDTO.Cpf != null && pedidoDTO.Cpf.Trim() != "") cliente = await _clienteRepository.ObterClientePorCpf(pedidoDTO.Cpf);
                else
                {
                    if (string.IsNullOrWhiteSpace(pedidoDTO.Descricao)) pedidoDTO.Descricao = "Obs: Cliente optou por não se identificar";
                    else pedidoDTO.Descricao += Environment.NewLine + "Obs: Cliente optou por não se identificar";
                }

                List<Produto> listaProdutos = [];

                foreach (var produtoDTO in pedidoDTO.Produtos)
                {
                    Produto prod = await _produtoRepository.ObterProdutosPorNome(produtoDTO.Nome)
                        ?? throw new DomainException($"Produto '{produtoDTO.Nome}' não encontrado.");

                    var produtosRepetidos = Enumerable.Range(0, produtoDTO.Quantidade).Select(_ => prod);
                    listaProdutos.AddRange(produtosRepetidos);
                }

                double valorTotal = listaProdutos.Sum(prod => prod.Preco);

                Pedido pedido = new("", listaProdutos, cliente, valorTotal, "AGUARDANDO PAGAMENTO", pedidoDTO.Descricao, DateTime.Now);
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
            if (status == "RECEBIDO" || status == "EM PREPARO" || status == "PRONTO" || status == "FINALIZADO")
            {
                var pedido = await _pedidoRepository.ObterPedidoPorNumero(numPedido) ?? throw new DomainException($"Não foi possível encontrar o pedido número '{numPedido}'.");
                if (pedido != null && (pedido.Status.Equals("AGUARDANDO PAGAMENTO") || pedido.Status.Equals("FINALIZADO"))) 
                    throw new DomainException($"Não foi possível atualizar o pedido número '{numPedido}' com o status {status}, pois o pedido está com o status {pedido.Status}.");
                if (pedido != null && pedido.Status.Equals(status)) 
                    throw new DomainException($"Não foi possível atualizar o pedido número '{numPedido}' com o status {status}, pois o status já está como {pedido.Status}.");               

                return true;
            }
            else throw new DomainException($"Não foi possível atualizar o pedido número '{numPedido}' com o status {status}.");
        }

    }
}
