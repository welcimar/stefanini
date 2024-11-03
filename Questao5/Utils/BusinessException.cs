    using System;
namespace Questao5.Utils
{

    public class BusinessException : Exception
    {
        public string Tipo { get; }

        public BusinessException(string message, string tipo = null) : base(message)
        {
            Tipo = tipo;
        }
    }

}
