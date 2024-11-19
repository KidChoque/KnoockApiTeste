using ApiKnoock.Domains;
using ApiKnoock.ViewModel;

namespace ApiKnoock.Interface
{
    public interface ICondominoRepository
    {
        void Create(CondominoViewModel condominoViewModel);

        Condomino SearchById(Guid id);

        List<Condomino> GetAllResident();
    }
}
