using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel // N�o � um Model e nem uma entidade do neg�cio. � um modelo auxiliar para povoar as telas
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}