using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //VAMOS criar uma dependencia para o SellerServices
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            //aqui ele vai cahamar o metodo FindAll() e retornar uma lista de Seller
            var list = _sellerService.FindAll();
            //aqui vamos passar esta lista list como argumento do método view, pra ele gerar
            //um IActionResult contendo esta lista list
            return View(list);
        }
    }
}
