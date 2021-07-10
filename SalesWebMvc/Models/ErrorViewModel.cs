using System;

namespace SalesWebMvc.Models
{
    public class ErrorViewModel
    {
        //esse RequestId é um Id interno da resuisição que agente pode mostrar também nessa
        //pagna de erro
        public string RequestId { get; set; }
        //essa pagina foi criada automaticamente. Vamos acrecentar esta prop abaixo
        //pra gente ter condição de acrecentar uma mensagem customizada nesse obj
        public string Message { get; set; }

        //essa função é pra testar se o Id existe. A função vai retornar se ele não
        //é nulo ou vazio
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}