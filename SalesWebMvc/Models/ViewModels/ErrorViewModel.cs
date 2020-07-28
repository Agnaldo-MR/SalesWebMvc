using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel // Não é um Model e nem uma entidade do negócio. É um modelo auxiliar para povoar as telas
    {
        public string RequestId { get; set; } // Id interno da requisição desta página de erro
        public string Message { get; set; } // Para acrescentar uma mensagem customizada nesta classe (objeto)

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // Retorna o id se ele não for nulo ou vazio
    }
}