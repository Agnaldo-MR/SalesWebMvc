using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

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
        private readonly SellerService _sellerService; // Criar uma dependência
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
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
            var departments = _departmentService.FindAll(); // Busca no BD todos os departamentos no serviço criado
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost] // Annotation para indicar que a ação abaixo vai ser uma ação de "Post" e não de "Get" 
        [ValidateAntiForgeryToken] // Para evitar ataque na seção de autenticação com dados maliciosos
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Para redirecionar depois de enviar o Form ao BD. Poderia ser só (RedirectToAction("Index"))
        }

        public IActionResult Delete(int? id) // "?" para indicar que é opcional
        {
            if(id == null) // Se for null a requisição foi feita de forma indevida
            {
                return NotFound(); // Para um resposta básica
            }

            var obj = _sellerService.FindById(id.Value); // Para pegar o valor caso exista "?"
            if (obj == null)
            {
                return NotFound(); // Para um resposta básica
            }

            return View(obj);
        }

        [HttpPost] // Annotation para indicar que a ação abaixo vai ser uma ação de "Post" e não de "Get" 
        [ValidateAntiForgeryToken] // Para evitar ataque na seção de autenticação com dados maliciosos
        public IActionResult Delete (int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) // Se for null a requisição foi feita de forma indevida
            {
                return NotFound(); // Para um resposta básica
            }

            var obj = _sellerService.FindById(id.Value); // Para pegar o valor caso exista "?"
            if (obj == null)
            {
                return NotFound(); // Para um resposta básica
            }

            return View(obj);
        }

        public IActionResult Edit(int? id) // int? para evitar acontecer algum erro de execução
        {
            if(id == null) // Testa se Id não existe
            {
                return NotFound(); // Provisório. O correto é retornar uma página personalizada de erro
            }

            var obj = _sellerService.FindById(id.Value); // Objeto buscado no BD
            if (obj == null) // Testa se Id é nulo
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost] // Annotation para indicar que a ação abaixo vai ser uma ação de "Post" e não de "Get" 
        [ValidateAntiForgeryToken] // Para evitar ataque na seção de autenticação com dados maliciosos
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index)); // Para redirecionar depois de enviar o Form ao BD. Poderia ser só (RedirectToAction("Index"))
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}