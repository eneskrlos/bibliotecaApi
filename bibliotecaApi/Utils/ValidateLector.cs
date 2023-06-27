using bibliotecaApi.Models;
using bibliotecaApi.Models.Request;
using bibliotecaApi.Utils.Exceptions;

namespace bibliotecaApi.Utils
{
    public class ValidateLector
    {
        public void ValidateDataLector(RequestLector lector) 
        {
            if (ContentInvalidCharacter(lector.Nombre)) throw new InvalidChracaterLectorNameException("El nombre del lector no puede tener caracteres invalidos");
        }

        public bool ContentInvalidCharacter(string name)
        {
            return (name.Contains("@") || name.Contains(":") || name.Contains(";")) ? true : false;
        }
    }
}
