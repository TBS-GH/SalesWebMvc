using System;

namespace SalesWebMvc.Models
{
    public class ErrorViewModel
    {
        //esse RequestId � um Id interno da resuisi��o que agente pode mostrar tamb�m nessa
        //pagna de erro
        public string RequestId { get; set; }
        //essa pagina foi criada automaticamente. Vamos acrecentar esta prop abaixo
        //pra gente ter condi��o de acrecentar uma mensagem customizada nesse obj
        public string Message { get; set; }

        //essa fun��o � pra testar se o Id existe. A fun��o vai retornar se ele n�o
        //� nulo ou vazio
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}