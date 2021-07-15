using System;

namespace SalesWebMvc.Services.Exceptions
{
    //essa é uma execessao personalizada de serviço para erros de integridade referencial
    public class IntegrityException : ApplicationException
    {
        public IntegrityException (string message) : base(message)
        {
        }
    }
}
