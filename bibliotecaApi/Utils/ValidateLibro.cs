using bibliotecaApi.Models;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Utils.Exceptions;

namespace bibliotecaApi.Utils
{
    public class ValidateLibro
    {
        public void ValidateBook(RequestLibro libro, BibliotecaDBContext bibliotecaDB)
        {
            if (BookExist(libro.ISBN, bibliotecaDB)) throw new BookIsAlreadyException($"El libro con ISBM: {libro.ISBN} ya existe");
            if (HaveEmptyValues(libro.Nombre, libro.ISBN)) throw new ExistEmptyElementsException("Existen campos que son requeridos.");
        }

        public void ValidateUpdateBook(LibroDTO libroDTO)
        {
            if (HaveEmptyValues(libroDTO.Nombre, libroDTO.ISBN)) throw new ExistEmptyElementsException("Existen campos que son requeridos.");
        }


        public void ValidateDeleteBook(Libro libro)
        {
            if (libro.Prestado) throw new NotDeleteException($"El libro {libro.Nombre} esta prestado no se puede eliminar");    
        }

        private bool BookExist(string iSBM, BibliotecaDBContext bibliotecaDB)
        {
            return bibliotecaDB.Libros.Any(x => x.ISBN.Equals(iSBM));
        }

        private bool HaveEmptyValues(string nombre, string isbn)
        {
            if (nombre.Equals("") || nombre.Equals(string.Empty) || nombre == null)
            {
                return true;
            }
            else if (isbn.Equals("") || isbn.Equals(string.Empty) || isbn == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
