using bibliotecaApi.Models.Request;
using bibliotecaApi.Models.Response;

namespace bibliotecaApi.Services.Interface
{
    public interface ILibroService
    {
        Task<Response> GetAllLibros();
        Task<Response> GetLibroById(Guid id);
        Task<Response> CreateBook(RequestLibro requestLibro);
        Task<Response> UpdateBook(LibroDTO requestLibro, Guid id);
        Task<Response> DeleteBook(Guid id);
    }
}
