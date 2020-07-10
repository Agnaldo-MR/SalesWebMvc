using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context; // "readonly" = para prevenir que esta dependência não possa ser alterada

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll() // Método para retornar uma lista dos departamentos ordenados por nome
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}