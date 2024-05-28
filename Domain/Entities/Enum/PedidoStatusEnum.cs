using System.ComponentModel;

namespace Domain.Entities.Enum
{
    public enum StatusPedidoEnum
    {
        [Description("AGUARDANDO PAGAMENTO")]
        AGUARDANDO_PAGAMENTO,

        [Description("RECEBIDO")]
        RECEBIDO,

        [Description("EM PREPARO")]
        EM_PREPARO,

        [Description("PRONTO")]
        PRONTO,

        [Description("FINALIZADO")]
        FINALIZADO
    }
}
