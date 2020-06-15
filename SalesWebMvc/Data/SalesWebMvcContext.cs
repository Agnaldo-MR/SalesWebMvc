using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SalesWebMvc.Models // Substitui o using SalesWebMvc.Models;
{
    public class SalesWebMvcContext : DbContext
    {
        public SalesWebMvcContext (DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } // Contexto para gravar as classes no DataBase
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
