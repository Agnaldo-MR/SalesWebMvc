using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel // N�o � um Model e nem uma entidade do neg�cio. � um modelo auxiliar para povoar as telas
    {
        public string RequestId { get; set; } // Id interno da requisi��o desta p�gina de erro
        public string Message { get; set; } // Para acrescentar uma mensagem customizada nesta classe (objeto)

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // Retorna o id se ele n�o for nulo ou vazio
    }
}