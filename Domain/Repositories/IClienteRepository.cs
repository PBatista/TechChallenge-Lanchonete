using Domain.Entities;

namespace Domain.Repositories
{
    public interface IClienteRepository
    {
        Task SalvarCliente(Cliente cliente);

        Task<Cliente> ObterClientePorCpf(string cpf);

        Task<List<Cliente>> ListarCliente();
    }
}
