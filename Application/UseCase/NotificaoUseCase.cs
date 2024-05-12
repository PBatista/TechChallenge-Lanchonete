using Application.IUseCase;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class NotificaoUseCase : INotificaoUseCase
    {       

        public Task NotificarClientePedidoPronto(string numPedido)
        {
            Console.WriteLine("Notificação enviada ao cliente");

            return Task.FromResult(true);
                       
        }
    }
}
