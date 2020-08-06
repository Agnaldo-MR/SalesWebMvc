using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context; // "readonly" = para prevenir que esta dependência não possa ser alterada

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        /* Para converter as operações que são síncronas em operações assíncrona
         * Quando é chamada a ação Index de SellersController e por sequência é chamada a ação FindAll do serviço,
         * o serviço por sua vez vai acessar o BD por meio do Entity Framework que é uma operação relativamente lenta.
         * Acessos a HD, BD ou rede é uma operação relativamente mais lenta que consome tempo da aplicação.
         * Se esta operação for síncrona a aplicação vai ficar bloqueada esperando a conclusão da operação para continuar 
         * em seguida. Então, operações mais lentas precisam ser assíncronas, sendo assim executadas separadamente 
         * dando continuidade à aplicação e melhorando muito a performance.
         */
        public async Task<List<Department>> FindAllAsync() // Método Assíncrono para retornar uma lista dos departamentos ordenados por nome
        { // public List<Department> FindAll() = Síncrona / Task = objeto para encapsular o processamento assíncrono facilitando a programação
          // sufixo FindAll"Async" = recomendação padrão adotada pela plataforma C#
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
            // await = para avisar ao compilador que é uma operação (chamada) assíncrona
            // ToList() é que vai executar a consulta e transforma o resultado para List (operação síncrona)
            // ToListAsync() outra versão (assíncrona) que não é do "Linq" e sim do entity framework (using Microsoft.EntityFrameworkCore)
        }
    }
}
/* “A Microsoft ADO.NET Entity Framework é um framework de mapeamento Objeto/Relacional (ORM) 
 * que permite aos desenvolvedores trabalhar com dados relacionais como objetos específicos de domínio,
 * eliminando a necessidade da maior parte do código de acesso de dados de que os desenvolvedores geralmente precisam escrever.
 * Usando o Entity Framework, os desenvolvedores realizam consultas utilizando o LINQ, em seguida,
 * recuperam e manipulam dados como objetos fortemente tipados.
 * A Implementação do ORM Entity Framework fornece serviços como controle de alterações, resolução de identidade,
 * lazy loading, tradução de consultas etc. de forma que os desenvolvedores podem se concentrar
 * na lógica de negócios da sua aplicação ao invés de nos fundamentos de acesso a dados”.
 *
 * Simplificando, podemos dizer que o Entity Framework é um framework O/RM que realiza o mapeamento
 * Objeto/Relacional (O/RM) partindo do modelo relacional de dados e gerando classes que representam as entidades do domínio.
 */
