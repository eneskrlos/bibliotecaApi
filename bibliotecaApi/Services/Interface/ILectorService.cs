using bibliotecaApi.Models.Request;
using bibliotecaApi.Models.Response;

namespace bibliotecaApi.Services.Interface
{
    public interface ILectorService
    {
        Task<Response> GetAllLectores();
        Task<Response> CreateLector(RequestLector lectorDTO);
        Task<Response> GetLectorById(Guid Id);
    }
}
