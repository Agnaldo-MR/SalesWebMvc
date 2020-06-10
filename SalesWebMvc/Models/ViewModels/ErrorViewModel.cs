using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel // Não é um Model e nem uma entidade do negócio. É um modelo auxiliar para povoar as telas
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}