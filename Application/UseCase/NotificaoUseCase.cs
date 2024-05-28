using Application.IUseCase;

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
