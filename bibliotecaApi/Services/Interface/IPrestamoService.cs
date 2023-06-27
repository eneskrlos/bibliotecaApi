using bibliotecaApi.Models.Response;

namespace bibliotecaApi.Services.Interface
{
    public interface IPrestamo
    {
        Task<Response> GetAllPrestamos();
        Task<Response> GetPrestamoById(Guid id);
    }
}
