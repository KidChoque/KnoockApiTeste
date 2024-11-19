using ApiKnoock.Interface;
using ApiKnoock.Repository;
using ApiKnoock.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnoock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfiliadosController : ControllerBase
    {
        private readonly IAfiliadosRepository _afiliadosRepository;

        public AfiliadosController()
        {
            _afiliadosRepository = new AfiliadosRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_afiliadosRepository.GetAllAffiliate());
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(AfiliadosViewModel afiliadosViewModel)
        {
            try
            {
                _afiliadosRepository.Create(afiliadosViewModel);
                return StatusCode(201, "Afiliado criado com sucesso.");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
