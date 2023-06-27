using bibliotecaApi.Models;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Models.Response;
using bibliotecaApi.Services.Interface;
using bibliotecaApi.Utils;
using bibliotecaApi.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaApi.Services
{
    public class LectorService : ILectorService
    {
        public readonly BibliotecaDBContext _bibliotecaContext;
        public readonly ValidateLector _validate = new();

        public LectorService(BibliotecaDBContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public async Task<Response> CreateLector(RequestLector lector)
        {
            try
            {
                _validate.ValidateDataLector(lector);
                var newLector = RequesttoDBO(lector);
                _bibliotecaContext.Lectores.Add(newLector);
                await _bibliotecaContext.SaveChangesAsync();
                return CorrectResponseApiData("Se inserto correctamente", newLector);
            }
            catch (InvalidChracaterLectorNameException e) 
            {
                return ErrorResponseApi(e.Message);
            }
            catch (Exception e)
            {
                return ErrorResponseApi(e.Message);
            }
        }

        

        public async Task<Response> GetAllLectores()
        {
            try
            {
                var listLector = await _bibliotecaContext.Lectores.Select(l => DBOtoDTO(l)).ToListAsync();
                return CorrectResponseApiData("Se listo correctamente", listLector);
            }
            catch (Exception e)
            {
                return ErrorResponseApi(e.Message);
            }
            
        }

        public async Task<Response> GetLectorById(Guid Id)
        {
            try
            {
                var currentLector = await _bibliotecaContext.Lectores.FindAsync(Id);
                if (currentLector == null)
                {
                    throw new ElementNotFoundException("No existe el Lector");
                }
                else
                {
                    var lectorDTO = DBOtoDTO(currentLector);
                    return CorrectResponseApiData("Operacion Exitosa", lectorDTO);
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

        private static LectorDTO DBOtoDTO(Lector lector)
        {
            return new LectorDTO
            {
                Id = lector.Id,
                Nombre = lector.Nombre
            };
        }

        private static Lector DTOtoDBO(LectorDTO lector)
        {
            return new Lector
            {
                Id = lector.Id,
                Nombre = lector.Nombre
            };
        }

        private static Lector RequesttoDBO(RequestLector lector)
        {
            return new Lector()
            {
                Nombre = lector.Nombre
            };
        }

        private static Response ErrorResponseApi(string mensaje) => new("ko", mensaje);
        private static Response CorrectResponseApiData(string mensaje, object data) => new("ok", mensaje, data);
        private static Response CorrectResponseApi(string mensaje) => new("ok", mensaje);
    }
}
