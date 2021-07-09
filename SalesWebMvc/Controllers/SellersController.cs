﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

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
        [HttpPost] //anotation de POST Action, quando não tiver essa anotação todo o resto vai ser da anotação Action GET

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

        //criando a logica do controle do link Delete
        public IActionResult Delete(int? id) //o int? significa que é opicional
        {
            //verifica se o id fornecido é nulo
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

        //criando a logica do controle do link Details
        public IActionResult Details(int? id) //o int? significa que é opicional
        {
            //verifica se o id fornecido é nulo
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

        //metodo Edit para abrir a tela para editar o vendedor
        public IActionResult Edit(int? id)
        {
            //verificando se o id informado é nulo
            if (id == null)
            {
                return NotFound();
            }
            //verificando se o id existe no DB
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            //passndo dos teste vamos abrir a tela de edição, para isso temos que abrir a caixa de 
            //departamentos para povoar minha caixa de seleção
            List<Department> departments = _departmentService.FindAll();

            //criando um obj do tipo SellerFormViewModel, já passando os dados. O Seller = obj, foi o
            //objeto que buscamos no DB, como estamos fazendo uma edição, vamos preencher com os dados
            //do vendedor pré-existente. E passamos também a lista de departamentos
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        //criando o metodo Edit com Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            //verificando se o id do URL for difernete do is do Seller
            if (id != seller.Id)
            {
                return BadRequest();
            }


            //essa chamada de .update() ela pode gerar excessões (tanto um NotFoudException quanto
            //um DbConcurrencyException). Portanto devemos fazer essa chamada dentro de um bloco
            //try
            try
            {
                _sellerService.Update(seller);
                //agora vamos redirecionar a requisição para pagina inicial do crude, que é a index
                return RedirectToAction(nameof(Index));
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
