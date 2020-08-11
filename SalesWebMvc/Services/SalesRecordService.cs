using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context; // "readonly" = para prevenir que esta dependência não possa ser alterada

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) // Datas opcionais
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue) // Se foi informada uma data mínima faça
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue) // Se foi informada uma data máxima faça
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller) // Para fazer o join das tabelas
                .Include(x => x.Seller.Department) // Para faer o join com departamentos
                .OrderByDescending(x => x.Date) // Ordenar por data
                .ToListAsync(); // Retornar uma lista assíncrona
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate) // Datas opcionais
        { // Para agrupar deve ser alterado o tipo (acima)
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue) // Se foi informada uma data mínima faça
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue) // Se foi informada uma data máxima faça
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller) // Para fazer o join das tabelas
                .Include(x => x.Seller.Department) // Para faer o join com departamentos
                .OrderByDescending(x => x.Date) // Ordenar por data
                .GroupBy(x => x.Seller.Department) // Para agrupar por departamento
                .ToListAsync(); // Retornar uma lista assíncrona
        }
    }
}
