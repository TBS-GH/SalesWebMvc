using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //VAMOS criar uma dependencia para o SellerServices
        private readonly SellerService _sellerService;

        //VAMOS criar uma dependencia para o DepartmentService
        private readonly DepartmentService _departmentService;

        //construtor
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //aqui ele vai cahamar o metodo FindAll() e retornar uma lista de Seller
            var list = _sellerService.FindAll();
            //aqui vamos passar esta lista list como argumento do método view, pra ele gerar
            //um IActionResult contendo esta lista list
            return View(list);
        }

        public IActionResult Create()
        {
            //aqui vamos ter que carregar e buscar do DB todos os departamentos no servico que criamos
            var departments = _departmentService.FindAll();
            
            //vamos instanciar um obj viewModel
            var viewModel = new SellerFormViewModel { Departments = departments };
            
            //feito isso vamos passar o viewModel para o View(), que qnd criar a tela de cadastro pela
            //primeira vez, ele ja vai receber o obj viewModel com os departamentos populados
            return View(viewModel);
        }

        //queremos indicar que essa ação de baixo (IActionResult Create(Seller seller))
        //seja do tipo POST e não de GET
        //pra fazer isso devemos colocar um anotation( [HttpPost] ) em cima ou antes do método
        [HttpPost] //anotation de POST

        //agora vamos colocar uma segunda anotação, para previnir que nossa aplicação
        //sofra um ataque de CSRF
        [ValidateAntiForgeryToken]
        //*********************
        //vamos criar uma sobrecarga do metodo create, ela vai receber um objeto vendedor
        //que veio na requisição, para que eu receba essa objeto da requisição e instacie esse 
        //vendendor basta colocar como parametro o framework ja faz automaticamente pra gente
        public IActionResult Create(Seller seller)
        {
            //aqui esta inserindo o obj seller
            _sellerService.Insert(seller);

            //agora vamos redirecionar minha requisição para a ação index, que é a ação que vai 
            //mostrar novamente na tela principal meu crude de vendedores
            //para redirecionar utilizamos o metodo RedirectToAction(), o framework aceita
            //colocar o nome do metodo entre aspas, como abaixo:
            //  return RedirectToAction("Index");

            //porem não é um procedimento muito legal, uma vez que se futuramente mudar o nome 
            //do metodo Index(), teremos que mudar lá também
            //Da forma descrito abaixo fica mais automatico e não precisa mudar nada aqui
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int? id) //o int? significa que é opicional
        {
            if (id == null)
            {
                return NotFound();
            }

            //buscando do banco de dados o vendedor
            var obj = _sellerService.FindById(id.Value);
            //se o id for nulo vamos apresentar esse erro
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //criando o metodo Delete com Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            //removendo o Seller do DB
            _sellerService.Remove(id);
            //vamos redirecionar para tela inical do meu crude
            return RedirectToAction(nameof(Index));
        }
    }
}
