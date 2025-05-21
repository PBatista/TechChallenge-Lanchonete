using Application.IGateways;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Gateways
{
    public class ClienteGateway : IClienteGateway
    {
        private readonly IClienteRepository _repository;

        public ClienteGateway(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            return await _repository.ListarCliente();
        }

        public async Task<Cliente> ObterClientePorCpf(string cpf)
        {
            return await _repository.ObterClientePorCpf(cpf);
        }

        public async Task SalvarCliente(Cliente cliente)
        {
            await _repository.SalvarCliente(cliente);
        }
    }
}
