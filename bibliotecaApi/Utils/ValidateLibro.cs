using bibliotecaApi.Models;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Utils.Exceptions;

namespace bibliotecaApi.Utils
{
    public class ValidateLibro
    {
        public void ValidateBook(RequestLibro libro, BibliotecaDBContext bibliotecaDB)
        {
            if (BookExist(libro.ISBN, libro.Nombre, bibliotecaDB)) throw new BookIsAlreadyException($"El libro ya existe");
            if (HaveEmptyValues(libro.Nombre, libro.ISBN)) throw new ExistEmptyElementsException("Existen campos que son requeridos.");
            if (IsInvalidISBN(libro.ISBN)) throw new InvalidIsbnException("El ISBN no es correctao. Debe tener 10 o 13 caracteres y no debe contener letras ni caracteres invalidos.");
        }

        public void ValidateUpdateBook(LibroDTO libroDTO)
        {
            if (HaveEmptyValues(libroDTO.Nombre, libroDTO.ISBN)) throw new ExistEmptyElementsException("Existen campos que son requeridos.");
            if (IsInvalidISBN(libroDTO.ISBN)) throw new InvalidIsbnException("El ISBN no es correctao. Debe tener 10 o 13 caracteres y no debe contener letras ni caracteres invalidos.");
        }


        public void ValidateDeleteBook(Libro libro)
        {
            if (libro.Prestado) throw new NotDeleteException($"El libro {libro.Nombre} esta prestado no se puede eliminar");
        }

        private bool BookExist(string iSBM, string nombre, BibliotecaDBContext bibliotecaDB)
        {
            return bibliotecaDB.Libros.Any(x => x.ISBN.Equals(iSBM) || x.Nombre.ToUpper().Equals(nombre.ToUpper()));
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

        private bool IsInvalidISBN(string isbn)
        {


            if (isbn.ToString().Length != 10 && isbn.ToString().Length != 13)
            {
                return true;
            }

            foreach (char s in isbn)
            {
                if (!char.IsDigit(s))
                {
                    return true;
                }
            }

            return false;
        }


    }
}
