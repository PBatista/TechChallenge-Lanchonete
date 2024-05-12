
namespace Application.IUseCase
{
    public interface INotificaoUseCase
    {
        Task NotificarClientePedidoPronto(string numPedido);
    }
}
