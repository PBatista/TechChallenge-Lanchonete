using Domain.Entities;

namespace Application.IGateways
{
    public interface IClienteGateway
    {
        Task<List<Cliente>> ListarClientes();
        Task<Cliente> ObterClientePorCpf(string cpf);
        Task SalvarCliente(Cliente cliente);
    }
}
