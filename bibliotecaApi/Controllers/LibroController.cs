using bibliotecaApi.Models;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Models.Response;
using bibliotecaApi.Services;
using bibliotecaApi.Utils.Enums;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly LibroService _libroService;

        public LibroController(LibroService libroService)
        {
            _libroService = libroService;
        }

        /// <summary>
        /// Metodo que permite listar todos los libros existentes.
        /// </summary>
        /// <returns>Response</returns>
        [HttpGet]
        public async Task<Response> GetLibros () {

            return await _libroService.GetAllLibros();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetLibroPorId(Guid id )
        {
            var response = await _libroService.GetLibroById(id);
            if(response.Code.Equals("ko")) 
            { 
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
            
        }

        /// <summary>
        /// Metodo que permite crear un libro.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response</returns>
        [HttpPost]
        public async Task<ActionResult<Response>> CreateLibro([FromBody] RequestLibro request)
        {
            var response = await _libroService.CreateBook(request);
            if (response.Code.Equals(CodeStatus.KO.ToString("G")))
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetLibroPorId), new { id = ((Libro)response.Data).Id }, ((Libro)response.Data));
        }


        /// <summary>
        /// Metodo que permite actualizar un libro.
        /// </summary>
        /// <param name="libroDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateLibro([FromBody] LibroDTO libroDTO, Guid id)
        {
            if(id != libroDTO.Id)
            {
                return BadRequest(new Response(CodeStatus.KO.ToString("G"), "No son correctos los identificadores"));
            }

            var response = await _libroService.UpdateBook(libroDTO, id);

            if (response.Code.Equals(CodeStatus.KO.ToString("G")))
            {
                return NotFound(response);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Metodo que permite eliminar un libro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteLibro(Guid id)
        {
            var response = await _libroService.DeleteBook(id);
            if (response.Code.Equals(CodeStatus.KO.ToString("G"))) 
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

    }
}
