using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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
            //obj.Department = _context.Department.First(); // Provisório para não ficar sem departamento. Desativado, pois, já existe a seleção de departamentos
            _context.Add(obj);
            _context.SaveChanges(); // Salvar no BD
        }

        public Seller FindById(int id) // Retorna o vendedor com o id solicitado. Se não existir retorna null
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); // Operação linq
        }                          // Join para vincular a classe Department

        public void Remove(int id) // Deleta o vendedor
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); // Remove o objeto do DBSet. Foi feito uma alteração
            _context.SaveChanges(); // Confirma a alteração para efetivação no BD pelo entitie framework 

        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) // Testar se o ID existe ou não no BD. Se não existir faça
            {
                throw new NotFoundException("Id not found (Id não encontrado)");
            }
            try
            {
            _context.Update(obj); // Se não entrar na exceção irá atualizar
            _context.SaveChanges(); // Confirmar os dados
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
