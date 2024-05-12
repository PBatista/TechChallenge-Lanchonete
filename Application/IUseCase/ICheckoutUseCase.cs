using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IUseCase
{
    public interface ICheckoutUseCase
    {
        Task ProcessarPagamento(string NumPedido);

    }
}
