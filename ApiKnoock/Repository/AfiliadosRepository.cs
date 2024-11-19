using ApiKnoock.Contexts;
using ApiKnoock.Domains;
using ApiKnoock.Interface;
using ApiKnoock.Utils;
using ApiKnoock.ViewModel;

namespace ApiKnoock.Repository
{
    public class AfiliadosRepository : IAfiliadosRepository
    {
        KnoockContext _context = new KnoockContext();
        public void Create(AfiliadosViewModel afiliadosViewModel)
        {
            try
            {
                // Cria o Usuario 
                var usuario = new Usuario
                {
                    Nome = afiliadosViewModel.Nome!,
                    Telefone = afiliadosViewModel.Telefone!,
                    Email = afiliadosViewModel.Email!,
                    Senha = Criptografia.Hash(afiliadosViewModel.Senha!),
                    DataNascimento = afiliadosViewModel.DataNascimento
                };

                // Salva as infomacoes
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                // Busca o tipo 'Afiliado' na tabela Tipo
                var tipoAfiliado = _context.Tipos.FirstOrDefault(t => t.Tipo1 == "Afiliado");
                if (tipoAfiliado == null) throw new Exception("Tipo 'Afiliado' não encontrado.");

                // Associa o usuário ao tipo 'Afiliado' em Tipo_Usuario
                var tipoUsuario = new TipoUsuario
                {
                    IdUsuario = usuario.Id,
                    IdTipo = tipoAfiliado.Id
                };

                // Salva as informacoes
                _context.TipoUsuarios.Add(tipoUsuario);
                _context.SaveChanges();

                // Cria o afiliado
                var afiliado = new Afiliado
                {
                    // Associacao do TipoUsuario
                    TipoUsuarioId = tipoUsuario.Id,
                    KnookCoins = afiliadosViewModel.KnoockCoins
                };

                _context.Afiliados.Add(afiliado);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

    

        public List<Afiliado> GetAllAffiliate()
        {
            try
            {
                return _context.Afiliados.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
