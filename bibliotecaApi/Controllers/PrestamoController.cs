using bibliotecaApi.Models;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Models.Response;
using bibliotecaApi.Services;
using bibliotecaApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly PrestamoService _service;
        public PrestamoController(PrestamoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Metodo que permite listar los prestamos que aun sus libros estan con estado prestado
        /// </summary>
        /// <returns>Devuelve una clase response donde lista los prestamos</returns>
        [HttpGet]
        public async Task<Response> GetPrestamos()
        {
            return await _service.GetAllPrestamos();
        }

        /// <summary>
        /// Metodo que permite obtener un prestamo por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve una clase response con el prestamo</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetPrestamoPorId(Guid id)
        {
            var resp = await _service.GetPrestamoById(id);
            if(resp.Code.Equals("ko"))
            {
                return BadRequest(resp.Message);
            }
            else 
            {
                return Ok(resp.Data);
            }
        }

        /// <summary>
        /// Metodo que permite realizar el prestamo
        /// </summary>
        /// <param name="request"></param>
        /// <returns>De vuelve una clase response</returns>
        [HttpPost("realizarPrestamo")]
        public async Task<ActionResult<Response>> RealizarPrestamo([FromBody] RequestPrestamo request)
        {
            var response = await _service.PrestarLibroToLector(request);
            if (response.Code.Equals("ko"))
            {
                return BadRequest(response.Message);
            }
            else 
            {
                return CreatedAtAction(nameof(GetPrestamoPorId), new { id = ((Prestamo)response.Data).Id }, response.Data);
            }
               
        }
    }
}
