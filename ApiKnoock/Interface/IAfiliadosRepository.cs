using ApiKnoock.Domains;
using ApiKnoock.ViewModel;

namespace ApiKnoock.Interface
{
    public interface IAfiliadosRepository
    {
        void Create(AfiliadosViewModel afiliadoViewModel);

        List<Afiliado> GetAllAffiliate();
    }
}
