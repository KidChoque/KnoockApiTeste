using ApiKnoock.Contexts;
using ApiKnoock.Domains;
using ApiKnoock.Interface;
using ApiKnoock.Utils;
using ApiKnoock.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ApiKnoock.Repository
{
    public class CondominoRepository : ICondominoRepository
    {
        KnoockContext _context = new KnoockContext();

        public void Create(CondominoViewModel condominoViewModel)
        {
            try
            {
                // Cria o Usuario 
                var usuario = new Usuario
                {
                    Nome = condominoViewModel.Nome!,
                    Telefone = condominoViewModel.Telefone!,
                    Email = condominoViewModel.Email!,
                    Senha = Criptografia.Hash(condominoViewModel.Senha!),
                    DataNascimento = condominoViewModel.DataNacimento
                };

                // Salva as infomacoes
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                // Busca o tipo 'Condomino' na tabela Tipo
                var tipoAfiliado = _context.Tipos.FirstOrDefault(t => t.Tipo1 == "Condomino");
                if (tipoAfiliado == null) throw new Exception("Tipo 'Condomino' não encontrado.");

                // Associa o usuário ao tipo 'Afiliado' em Tipo_Usuario
                var tipoUsuario = new TipoUsuario
                {
                    IdUsuario = usuario.Id,
                    IdTipo = tipoAfiliado.Id
                };

                // Salva as informacoes
                _context.TipoUsuarios.Add(tipoUsuario);
                _context.SaveChanges();

                // Cria o Condomino
                var condomino = new Condomino
                {
                    // Associacao do TipoUsuario
                    TipoUsuarioId = tipoUsuario.Id,
                    DeliveryPin = condominoViewModel.DeliveryPin,
                    Pin = condominoViewModel.Pin,
                    Bloco = condominoViewModel.Bloco,
                    Apartamento = condominoViewModel.Apartamento

                };

                
                _context.Condominos.Add(condomino);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Condomino> GetAllResident()
        {
            try
            {
                return _context.Condominos.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Condomino SearchById(Guid id)
        {
            try
            {
                return _context.Condominos.FirstOrDefault(x => x.Id == id)!;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
