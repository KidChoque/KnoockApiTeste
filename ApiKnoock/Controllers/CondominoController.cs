using ApiKnoock.Interface;
using ApiKnoock.Repository;
using ApiKnoock.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnoock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondominoController : ControllerBase
    {
        private readonly ICondominoRepository _condominoRepository;

        public CondominoController()
        {
            _condominoRepository = new CondominoRepository();
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_condominoRepository.GetAllResident());
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }


        [HttpGet("Id")]
        public IActionResult GetId(Guid id)
        {
            try
            {
                return Ok(_condominoRepository.SearchById(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost]

        public IActionResult Post (CondominoViewModel condominoViewModel)
        {
            try
            {
                _condominoRepository.Create(condominoViewModel);
                return StatusCode(201, "Condomino criado com sucesso.");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
