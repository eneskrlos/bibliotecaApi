using bibliotecaApi.Models.Request;
using bibliotecaApi.Models.Response;

namespace bibliotecaApi.Services.Interface
{
    public interface IAccionPrestamo
    {
        Task<Response> PrestarLibroToLector(RequestPrestamo request);
    }
}
