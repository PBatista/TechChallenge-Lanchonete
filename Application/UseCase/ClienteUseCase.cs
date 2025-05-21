using Application.ApplicationDTO;
using Application.IGateways;
using Application.IUseCase;
using Domain.Base;
using Domain.Entities;

namespace Application.UseCase
{
    public class ClienteUseCase : IClienteUseCase
    {
        private readonly IClienteGateway _gateway;

        public ClienteUseCase(IClienteGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            try
            {
                return await _gateway.ListarClientes();
            }
            catch (Exception ex)
            {
                throw new DomainException("Não foi possível listar os clientes", ex);
            }
        }

        public async Task<Cliente> ObterClientePorCpf(string cpf)
        {
            try
            {
                var formatado = AssertionConcern.RemoveNumbers(cpf);
                return await _gateway.ObterClientePorCpf(formatado);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Não foi possível obter o cliente com CPF '{cpf}'", ex);
            }
        }

        public async Task SalvarCliente(ClienteDTO dto)
        {
            try
            {
                var cpf = AssertionConcern.RemoveNumbers(dto.Cpf);
                var clienteExistente = await _gateway.ObterClientePorCpf(cpf);

                if (clienteExistente != null)
                    throw new DomainException($"O CPF '{dto.Cpf}' já está cadastrado.");

                var novoCliente = new Cliente(dto.Nome, cpf, dto.Email);
                await _gateway.SalvarCliente(novoCliente);
            }
            catch (Exception ex)
            {
                throw new DomainException("Não foi possível salvar o cliente.", ex);
            }
        }
    }
}
