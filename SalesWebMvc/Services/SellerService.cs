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

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // public List<Seller> FindAll() // Síncrono
        public async Task<List<Seller>> FindAllAsync() // Assíncrono
        {
            return await _context.Seller.ToListAsync(); // Para retornar do BD todos os vendedores.
                                                        // "Seller"=acessa a tabela "vendedores" e converte para uma lista (ToList)
        }

        // public void Insert(Seller obj) // Síncrono
        public async Task InsertAsync(Seller obj) //Insere formulário de Create.cshtml no BD // Assíncrono
        {
            //obj.Department = _context.Department.First(); // Provisório para não ficar sem departamento. Desativado, pois, já existe a seleção de departamentos
            _context.Add(obj); // Realizada só na memória
            await _context.SaveChangesAsync(); // Acessar e Salvar no BD // Assíncrona
        }

        // public Seller FindById(int id) // Síncrono
        public async Task<Seller> FindByIdAsync(int id) // Retorna o vendedor com o id solicitado. Se não existir retorna null
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); // Operação linq
        }                          // Join para vincular a classe Department

        // public void Remove(int id) // Síncrono
        public async Task RemoveAsync(int id) // Deleta o vendedor
        { // Interceptar a exceção DbUpdateException que será lançada pelo entity framework quando ocorrer uma violação
          // de integridade referencial e lançar uma exceção personalizada no nível de serviço que a IntegrityException
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj); // Remove o objeto do DBSet. Foi feito uma alteração
                await _context.SaveChangesAsync(); // Confirma a alteração para efetivação no BD pelo entitie framework
            }
            // catch(DbUpdateException e)
            catch (DbUpdateException)
            {
                // throw new IntegrityException(e.Message);
                throw new IntegrityException("Can`t delete seller because he/she has sales (Erro de integridade)"); // Personalizada
            }
        }

        // public void Update(Seller obj) // Síncrono
        public async Task UpdateAsync(Seller obj)
        {
            // if (!_context.Seller.Any(x => x.Id == obj.Id)) // Testar se o ID existe ou não no BD. Se não existir faça
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) // Testar se o ID existe ou não no BD. Se não existir faça
            {
                throw new NotFoundException("Id not found (Id não encontrado)");
            }
            try
            {
                _context.Update(obj); // Se não entrar na exceção irá atualizar
                await _context.SaveChangesAsync(); // Confirmar os dados
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
