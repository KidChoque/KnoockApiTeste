using ApiKnoock.Domains;
using ApiKnoock.Interface;
using ApiKnoock.Repository;
using ApiKnoock.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnoock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }


        [HttpPost]
        public IActionResult Post(UsuarioViewModel usuarioViewModel)
        {
            try
            {
                
                Usuario usuario = new Usuario
                {
                    
                    Nome = usuarioViewModel.Nome!,
                    Telefone = usuarioViewModel.Telefone!,
                    Email = usuarioViewModel.Email!,
                    Senha = usuarioViewModel.Senha!,
                    DataNascimento = usuarioViewModel.DataNacimento!,
                    CodigoRecuperacao =  usuarioViewModel.CodigoRecuperacao!
                };

                _usuarioRepository.Create(usuario);
                return StatusCode(201, usuarioViewModel);
            }
            catch (Exception error)
            {
                if (error.Message == "Email já cadastrado.")
                {
                    return BadRequest("O email informado já está em uso.");
                }

                return BadRequest(error.Message);
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                _usuarioRepository.SearchById(id);

                return Ok(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword(string email, ChangePasswordViewModel changePasswordViewModel)
        {

            try
            {
                _usuarioRepository.ChangePassword(email, changePasswordViewModel.NovaSenha!);

                return Ok("Senha Alterada com sucesso ");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
