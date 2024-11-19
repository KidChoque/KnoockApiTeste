using ApiKnoock.Contexts;
using ApiKnoock.Domains;
using ApiKnoock.Interface;
using ApiKnoock.Utils;

namespace ApiKnoock.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        KnoockContext _context = new KnoockContext();
        public bool ChangePassword(string email, string senha)
        {
            try
            {
                var user = _context.Usuarios.FirstOrDefault(x => x.Email == email);

                if (user == null)
                {
                    return false;
                }

                user.Senha = Criptografia.Hash(senha);

                _context.Update(user);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Create(Usuario usuario)
        {
            try
            {
                usuario.Senha = Criptografia.Hash(usuario.Senha!);
                _context.Add(usuario);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario Login(string email, string senha)
        {
            try
            {
                var user = _context.Usuarios.Select(x => new Usuario
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Senha = x.Senha,
                    Telefone = x.Telefone,
                    Email = x.Email,
                    DataNascimento = x.DataNascimento
                }).FirstOrDefault(y => y.Email == email);

                if (user == null)
                {
                    return null!;

                }

                if (!Criptografia.CompareHash(senha, user.Senha)) return null!;

                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario SearchById(Guid id)
        {
            try
            {
                return _context.Usuarios.FirstOrDefault(x => x.Id == id)!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
