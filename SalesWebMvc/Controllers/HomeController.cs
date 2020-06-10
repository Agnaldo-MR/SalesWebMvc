using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class HomeController : Controller // HomeController = A caminho da URL vai conter a palavra "Home"
    {
        // Cada um dos nomes dos métodos abaixo representa a própria ação
        public IActionResult Index() // IActionResult = Resultado de uma ação. ASPNET fortemente baseado em padrões de nomes
        {                            // IActionResult é uma interface de um super tipo genérico para todo resultado de alguma ação
                                     // View() retorna um objeto do tipo "ViewResult" que pode substituir o "IActionResult" que é mais genérico
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Salles Web MVC App from C# Course."; // Acessa um objeto "ViewData" na chave "Message" e recebe um valor
            // "ViewData"= Dicionário do c#. Coleção de chaves pares-chaves-valor
            ViewData["Aluno"] = " : Agnaldo Marlon Rodrigues"; // Acréscimo de valor ao ViewData através da chave "Email"
            return View(); // View() método auxiliar (meta de builder) para retornar um objeto do tipo "IActionResult", neste caso uma view
            /* 
             * Ação do framework: se está sendo instanciada uma view e está em uma ação about, o framework vai procurar na pasta "Views"
             * na subpasta "Home" (nome do controlador: HomeController) uma página com o nome "About".
             * Esta página About é que vai ser construída com o template Engine do framework.
             * As chaves "Message" e "Email" precisão ser declaradas na página About.
             * O sistema de template processará o template "About" em "Home" e trocará as referências das chaves pelos
             * valores deste controlador
             * Natural Templates: quando é digitado o camimho no navegador (URL) o controlador é que será chamado e depois as páginas (em home) 
             */
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
