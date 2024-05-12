using System.ComponentModel;

namespace Domain.Entities.Enum
{
    public enum PedidoStatusEnum
    {
        [Description("Recebido")]
        Recebido,

        [Description("Em Preparação")]
        Em_Preparo,

        [Description("Pronto")]
        Pronto,

        [Description("Finalizado")]
        Finalizado
    }
}
