using Application.IUseCase;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCase
{
    public class ClienteUseCase : IClienteUseCase
    {
        public readonly IClienteRepository _clienteRepository;

        public ClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            try
            {
                return await _clienteRepository.ListarCliente();
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível listar os clientes", ex);
            }            
        }

        public async Task<Cliente> ObterClientePorCpf(string cpf)
        {
            try
            {                
                return await _clienteRepository.ObterClientePorCpf(AssertionConcern.RemoveNumbers(cpf));
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi obter o cliente pelo cpf '{cpf}'", ex);
            }            
        }

        public async Task SalvarCliente(Cliente cliente)
        {
            try
            {
                var clienteExistente = await _clienteRepository.ObterClientePorCpf(cliente.Cpf);
                if(clienteExistente == null) await _clienteRepository.SalvarCliente(cliente);
                else throw new DomainException($"Não foi possível salvar o cliente, pois o CPF {cliente.Cpf} já está cadastrado!");
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível salvar o cliente", ex);
            }            
        }
    }
}
