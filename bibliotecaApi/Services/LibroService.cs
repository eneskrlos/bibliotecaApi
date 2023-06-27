using Azure.Core;
using bibliotecaApi.Models;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Models.Response;
using bibliotecaApi.Services.Interface;
using bibliotecaApi.Utils;
using bibliotecaApi.Utils.Enums;
using bibliotecaApi.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaApi.Services
{
    public class LibroService : ILibroService 
    {
        public readonly BibliotecaDBContext _bibliotecaContext;
        private readonly ValidateLibro _validate = new();

        public LibroService(BibliotecaDBContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public async Task<Response> GetAllLibros()
        {
            try
            {
                var listLibros = await _bibliotecaContext.Libros.Select(a => DBOtoDTO(a)).ToListAsync();

                return CorrectResponseApiData("Se muestra la lista", listLibros);
            }
            catch (Exception e)
            {

                return ErrorResponseApi(e.Message);
            }
        }

        
        public async Task<Response> GetLibroById(Guid id)
        {
            try
            {
                var libro = await _bibliotecaContext.Libros.FindAsync(id);
                if (libro != null)
                {
                    var libroDto = DBOtoDTO(libro);
                    return CorrectResponseApiData("Operacion Exitosa", libroDto);
                }
                else
                {
                    throw new ElementNotFoundException("No existe el libro");
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


        public async Task<Response> CreateBook(RequestLibro libro)
        {
            try
            {
                _validate.ValidateBook(libro, _bibliotecaContext);
                var newlibro = RequesttoDBO(libro);
                _bibliotecaContext.Libros.Add(newlibro);
                await _bibliotecaContext.SaveChangesAsync();
                return CorrectResponseApiData("Se inserto correctamente", newlibro);
            }
            catch (ExistEmptyElementsException e)
            {
                return ErrorResponseApi(e.Message);
            }
            catch (InvalidIsbnException e)
            {
                return ErrorResponseApi(e.Message);
            }
            catch (BookIsAlreadyException b)
            {
                return ErrorResponseApi(b.Message);
            }
            catch (Exception e)
            {
                return ErrorResponseApi(e.Message);
            }

        }

        public async Task<Response> UpdateBook(LibroDTO requestLibro, Guid id)
        {
            try
            {
                _validate.ValidateUpdateBook(requestLibro);

                var currentBook = await _bibliotecaContext.Libros.FindAsync(id);

                if (currentBook != null)
                {
                    var updateBook = DTOtoDBO(requestLibro);
                    currentBook.Nombre = updateBook.Nombre;
                    currentBook.ISBN = updateBook.ISBN;
                    currentBook.Prestado = updateBook.Prestado;

                    _bibliotecaContext.Update(currentBook);
                    await _bibliotecaContext.SaveChangesAsync();

                    return CorrectResponseApi("Se actualizo correctamente");
                }
                else
                {
                    throw new ElementNotFoundException("No existe el elemento");
                }
            }
            catch (ExistEmptyElementsException e)
            {
                return ErrorResponseApi(e.Message);
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

        public async Task<Response> DeleteBook(Guid id)
        {
            try
            {
                var currentBook = await _bibliotecaContext.Libros.FindAsync(id);

                if (currentBook == null)
                {
                    throw new ElementNotFoundException("No existe el elemento");
                }
                else
                {
                    _validate.ValidateDeleteBook(currentBook);
                    _bibliotecaContext.Libros.Remove(currentBook);
                    await _bibliotecaContext.SaveChangesAsync();

                    return CorrectResponseApi("Se elimino correctamente");
                }
            }
            catch (NotDeleteException e)
            {
                return ErrorResponseApi(e.Message);
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

       


        private static ResponseLibros DBOtoDTO(Libro libro)
        {
            return new ResponseLibros
            {
                Id = libro.Id,
                Nombre = libro.Nombre,
                ISBN = libro.ISBN,
                Prestado = libro.Prestado
            };
        }

        private static Libro DTOtoDBO(LibroDTO requestLibro)
        {
            return new Libro()
            {
                Id = requestLibro.Id,
                Nombre = requestLibro.Nombre,
                ISBN = requestLibro.ISBN,
                Prestado = requestLibro.Prestado
            };
        }

        private static Libro RequesttoDBO(RequestLibro request)
        {
            return new Libro()
            {
                Nombre = request.Nombre,
                ISBN = request.ISBN,
                Prestado = false
            };
        }


        private static Response ErrorResponseApi(string mensaje) => new(CodeStatus.KO.ToString("G"), mensaje);
        private static Response CorrectResponseApiData(string mensaje, object data) => new(CodeStatus.OK.ToString("G"), mensaje, data);
        private static Response CorrectResponseApi(string mensaje) => new(CodeStatus.OK.ToString("G"), mensaje);

        
    }
}
