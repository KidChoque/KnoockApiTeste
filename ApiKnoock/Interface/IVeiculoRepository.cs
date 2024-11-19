using ApiKnoock.Domains;

namespace ApiKnoock.Interface
{
    public interface IVeiculoRepository
    {
        void Create(Veiculo veiculo);

        void Delete(Guid id);

        List<Veiculo> GetAllVehicles();
    }
}
