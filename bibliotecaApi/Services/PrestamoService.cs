using Azure.Core;
using bibliotecaApi.Models;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Models.Response;
using bibliotecaApi.Services.Interface;
using bibliotecaApi.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaApi.Services
{
    public class PrestamoService : IPrestamo, IAccionPrestamo
    {
        public readonly BibliotecaDBContext _bibliotecaContext;

        public PrestamoService(BibliotecaDBContext bibliotecaContext) 
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public async Task<Response> GetAllPrestamos()
        {
            //Obtengop una lista de todos los prestamos donde aun los libros estan prestados.
            var prestamo = await _bibliotecaContext.Prestamos
                .Include(x => x.LibroNavigation)
                .Include(y => y.LectorNavigation)
                .Where(x => x.LibroNavigation.Prestado == true)
                .Select(x => DBOtoResponseDTO(x))
                .ToListAsync();
            return CorrectResponseApiData("Se listo las lista", prestamo);
            
        }

        public async Task<Response> GetPrestamoById(Guid id)
        {
            var prestamo = await _bibliotecaContext.Prestamos.FindAsync(id);
            if(prestamo == null)
            {
                throw new ElementNotFoundException("No existe el prestamo");
            }
            else
            {
                var prestamoDto = DBOtoDTO(prestamo);
                return CorrectResponseApiData("Operacion Exitosa", prestamoDto); 
            }
        }

        public async Task<Response> PrestarLibroToLector(RequestPrestamo request)
        {
            try
            {

                var prestamo = RequesttoDBO(request);
                var currentBook = await _bibliotecaContext.Libros.FindAsync(prestamo.IdLibro);
                var currentLector = await _bibliotecaContext.Lectores.FindAsync(prestamo.LectorId);
                if (currentBook == null)
                {
                    throw new ElementNotFoundException("No existe el libro");
                }
                else if (currentLector == null)
                {
                    throw new ElementNotFoundException("No existe el lector");
                }
                else
                {
                    
                    if (currentBook.Prestado)
                    {
                        return ErrorResponseApi("El libro ya esta prestado");
                    }
                    else
                    {
                        prestamo.LectorNavigation = currentLector;
                        prestamo.LibroNavigation = currentBook;
                        await _bibliotecaContext.Prestamos.AddAsync(prestamo);
                        await _bibliotecaContext.SaveChangesAsync();

                        currentBook.Prestado = true;
                        _bibliotecaContext.Libros.Update(currentBook);
                        await _bibliotecaContext.SaveChangesAsync();
                        return CorrectResponseApiData("Se registro el prestamo", prestamo);
                    }
                }
            }
            catch (ElementNotFoundException e) 
            {
                return ErrorResponseApi(e.Message);
            }
            catch (Exception e)
            {
                return ErrorResponseApi(e.Message);
            }
        }

        private static Prestamo DTOtoDBO(PrestamoDTO dTO)
        {
            return new Prestamo() 
            {
                Id = dTO.Id,
                IdLibro = dTO.IdLibro,
                LectorId = dTO.IdLector,
                FechaPrestamo = dTO.FechaPrestamo,
            };
        }

        private static Prestamo RequesttoDBO(RequestPrestamo request)
        {
            return new Prestamo()
            {
                IdLibro = request.IdLibro,
                LectorId = request.IdLector,
                FechaPrestamo = request.FechaPrestamo,
            };
        }

        private static PrestamoDTO DBOtoDTO(Prestamo prestamo)
        {
            return new PrestamoDTO()
            {
                Id = prestamo.Id,
                IdLibro = prestamo.IdLibro,
                IdLector = prestamo.LectorId,
                FechaPrestamo = prestamo.FechaPrestamo,
            };
        }

        private static ResponsePrestamo DBOtoResponseDTO(Prestamo prestamo)
        {
            return new ResponsePrestamo()
            {
                Id = prestamo.Id,
                FechaPrestamo = prestamo.FechaPrestamo,
                ISBN = prestamo.LibroNavigation.ISBN,
                Nombre = prestamo.LectorNavigation.Nombre
            };
        }

        private static Response ErrorResponseApi(string mensaje) => new("ko", mensaje);
        private static Response CorrectResponseApiData(string mensaje, object data) => new("ok", mensaje, data);
        private static Response CorrectResponseApi(string mensaje) => new("ok", mensaje);

       
    }
}
