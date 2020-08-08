using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class SellersController : Controller // Controlador = precisa respeitar o padrão de nomes do framework
    {
        private readonly SellerService _sellerService; // Criar uma dependência
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // public IActionResult Index() // Síncrono
        public async Task<IActionResult> Index() // Para ser assíncrono não será alterado para IndexAsync por ser um controlador
        {
            var list = await _sellerService.FindAllAsync(); // VAR lista de Seller
            return View(list); // View("list") passa o argumento para gerar uma IActionResult contendo a list
                               // É chamado o controlador "Index", o controlador acessa o "Model", 
                               // pega o dado na list e encaminha para a View e acontece a dinâmica do MVC.
        }

        // public IActionResult Create() // Síncrono
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync(); // Busca no BD todos os departamentos no serviço criado
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost] // Annotation para indicar que a ação abaixo vai ser uma ação de "Post" e não de "Get" 
        [ValidateAntiForgeryToken] // Para evitar ataque na seção de autenticação com dados maliciosos
        // public IActionResult Create(Seller seller) // Síncrono
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid) // Testar se o modelo foi validado. Se o formulário de cadastro foi preenchido corretamente
            { // Utilizado para validar os dados pelo servidor no caso do java script estar desabilitado no navegador do usuário
                var departments = await _departmentService.FindAllAsync(); // Carregar os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index)); // Para redirecionar depois de enviar o Form ao BD. Poderia ser só (RedirectToAction("Index"))
        }

        // public IActionResult Delete(int? id) // Síncrono
        public async Task<IActionResult> Delete(int? id) // "?" para indicar que é opcional
        {
            if (id == null) // Se for null a requisição foi feita de forma indevida
            {
                //return NotFound(); // Para um resposta básica
                return RedirectToAction(nameof(Error), new { message = "Id not provided (Id não fornecido)" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // Para pegar o valor caso exista "?"
            if (obj == null)
            {
                //return NotFound(); // Para um resposta básica
                return RedirectToAction(nameof(Error), new { message = "Id not found (Id não existe)" });
            }

            return View(obj);
        }

        [HttpPost] // Annotation para indicar que a ação abaixo vai ser uma ação de "Post" e não de "Get" 
        [ValidateAntiForgeryToken] // Para evitar ataque na seção de autenticação com dados maliciosos
        // public IActionResult Delete (int id) // Síncrono
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // public IActionResult Details(int? id) // Síncrono
        public async Task<IActionResult> Details(int? id) // Ação de detalhe do seller
        {
            if (id == null) // Se for null a requisição foi feita de forma indevida
            {
                //return NotFound(); // Para um resposta básica
                return RedirectToAction(nameof(Error), new { message = "Id not provided (Id não fornecido)" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // Para pegar o valor caso exista "?"
            if (obj == null)
            {
                //return NotFound(); // Para um resposta básica
                return RedirectToAction(nameof(Error), new { message = "Id not found (Id não existe)" });
            }

            return View(obj);
        }

        // public IActionResult Edit(int? id) // Síncrono 
        public async Task<IActionResult> Edit(int? id) // int? para evitar acontecer algum erro de execução
        {
            if (id == null) // Testa se Id não existe
            {
                //return NotFound(); // Provisório. O correto é retornar uma página personalizada de erro
                return RedirectToAction(nameof(Error), new { message = "Id not provided (Id não fornecido)" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // Objeto buscado no BD
            if (obj == null) // Testa se Id é nulo
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found (Id não existe)" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost] // Annotation para indicar que a ação abaixo vai ser uma ação de "Post" e não de "Get" 
        [ValidateAntiForgeryToken] // Para evitar ataque na seção de autenticação com dados maliciosos
        // public IActionResult Edit(int id, Seller seller) // Síncrono
        public async Task<IActionResult> Edit(int id, Seller seller) // Ação de edição do seller
        {
            if (!ModelState.IsValid) // Testar se o modelo foi validado. Se o formulário de cadastro foi preenchido corretamente
            { // Utilizado para validar os dados pelo servidor no caso do java script estar desabilitado no navegador do usuário
                var departments = await _departmentService.FindAllAsync(); // Carregar os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { message = "Id mismatch (Id`s não correspondem)" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index)); // Para redirecionar depois de enviar o Form ao BD. Poderia ser só (RedirectToAction("Index"))
            }
            catch (NotFoundException e)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            /*Os dois catch`s acima pode ser substituído abaixo, pois, atende à ambas situações:
             * 
             * catch (ApplicationException e)
             * return RedirectToAction(nameof(Error), new { message = e.Message });
             */
        }

        public IActionResult Error(string message) // Ação de erro. Não precisa ser assíncrona, pois, não tem acesso à dados
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // Recurso para pegar o ID interno da requisição
            };
            return View(viewModel);
        }
    }
}