using Application.ApplicationDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IPresenters
{
    public interface IClientePresenter
    {
        List<ClienteDTO> PresentMany(List<Cliente> clientes);
        ClienteDTO Present(Cliente cliente);
    }
}
