using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

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
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); // VAR lista de Seller
            return View(list); // View("list") passa o argumento para gerar uma IActionResult contendo a list
                               // É chamado o controlador "Index", o controlador acessa o "Model", 
                               // pega o dado na list e encaminha para a View e acontece a dinâmica do MVC.
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Annotation para indicar que a ação abaixo vai ser uma ação de "Post" e não de "Get" 
        [ValidateAntiForgeryToken] // Para evitar ataque na seção de autenticação com dados maliciosos
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Para redirecionar depois de enviar o Form ao BD. Poderia ser só (RedirectToAction("Index"))
        }
    }
}