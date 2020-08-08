using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        // Exceção personalizada de serviço para erros de integridade referencial
        public IntegrityException(string message) : base(message) /// Construtor
        {
        }
    }
}