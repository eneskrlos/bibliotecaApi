using bibliotecaApi.Services;
using bibliotecaApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Models;
using bibliotecaApi.Utils.Enums;

namespace bibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectorController : ControllerBase
    {
        private readonly LectorService _lectorService;

        public LectorController(LectorService lectorService)
        {
            _lectorService = lectorService;
        }


        /// <summary>
        /// Metodo que permite obtener todos los lectores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Response> GetLectores()
        {
            return await _lectorService.GetAllLectores();
        }

        /// <summary>
        /// Metodo que permite obtener un lector
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetLectorPorId(Guid id)
        {
            var response = await _lectorService.GetLectorById(id);
            if(response.Code.Equals(CodeStatus.KO.ToString("G")))
            {
                return BadRequest(response.Message);
            }
            else
            {
                return Ok(response.Data);
            }
        }

        /// <summary>
        /// Metodo que permite crear un lector
        /// </summary>
        /// <param name="lector"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response>> CreateLector([FromBody] RequestLector lector)
        {
            var response = await _lectorService.CreateLector(lector);
            if (response.Code.Equals(CodeStatus.KO.ToString("G")))
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetLectorPorId), new { id = ((Lector)response.Data).Id}, (Lector)response.Data);
        }

    }
}
