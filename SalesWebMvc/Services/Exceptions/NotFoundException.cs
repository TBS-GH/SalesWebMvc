using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //criamos aqui uma excessão personalizada NotFound
        //isso é importante pois temos um controle maior, pra cada tipo de excessão que pode ocorrer
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
