using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context; // "readonly" = para prevenir que esta dependência não possa ser alterada

        public SellerService (SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); // Para retornar do BD todos os vendedores.
                                             // "Seller"=acessa a tabela "vendedores" e converte para uma lista (ToList)
        }

        public void Insert(Seller obj) //Insere formulário de Create.cshtml no BD
        {
            _context.Add(obj);
            _context.SaveChanges(); // Salvar no BD
        }
    }
}
