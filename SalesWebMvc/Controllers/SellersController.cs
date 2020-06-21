using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

/* Observações:
 * Quando é acionado o link "Sellers" no navegador, a chamada é recebida por esta classe do controlador
 * Quando não especificada uma ação, a ação "Index" será acionada (IActionResult) que solicita retornar a chamada do View (return)
 * A ação view irá retornar um objeto "IActionResult" considerando o nome de view "Index"
 * Será então acionada a view da pasta "Sellers" que contém o nome "Index.cshtml" que resultará o retorno visual da página no navegador 
 * Conclusão: o controlador encaminhou a requisição para a view. Assim é o funcionamento no modelo "MVC".
 */
namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
