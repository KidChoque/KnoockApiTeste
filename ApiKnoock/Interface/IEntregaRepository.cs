using ApiKnoock.Domains;

namespace ApiKnoock.Interface
{
    public interface IEntregaRepository
    {
        void Create(Entrega entrega);

        List<Entrega> GetAllDelivery();
    }
}
