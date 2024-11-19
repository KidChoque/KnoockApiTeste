using System;
using System.Collections.Generic;

namespace ApiKnoock.Domains;

public partial class NotificacaoEntrega
{
    public Guid NotificacaoId { get; set; }

    public Guid? EntregaId { get; set; }

    public virtual Entrega? Entrega { get; set; }

    public virtual Notificacao Notificacao { get; set; } = null!;
}
