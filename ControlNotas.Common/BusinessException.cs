using System;
using System.Collections.Generic;
using System.Text;

namespace ControlNotas.Common
{
    public class BusinessException : Exception //Excepción personalizada
    {
        public string CodigoError { get; } // La propiedad de la clase

        public BusinessException(string mensaje, string codigoError) : base(mensaje) // Base llama a el constructor de Exception
        {
            CodigoError = codigoError; // Le asignamos el valor que viene de parametro a la propiedad de la clase
        }
    }
}
