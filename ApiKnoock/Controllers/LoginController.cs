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
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                Usuario searchUser = _usuarioRepository.Login(loginViewModel.Email!, loginViewModel.Senha!);
                if (searchUser == null)
                {
                    return StatusCode(401, "Email ou Senha Inválidos");
                }

                var response = new
                {
                    user = searchUser,
                    message = "Login Successful"
                };

                return Ok(response);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}



