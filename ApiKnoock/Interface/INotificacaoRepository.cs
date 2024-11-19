using ApiKnoock.Domains;

namespace ApiKnoock.Interface
{
    public interface INotificacaoRepository
    {
        void Create(Notificacao notificacao);

        List<Notificacao> GetAllNotification();
    }
}
