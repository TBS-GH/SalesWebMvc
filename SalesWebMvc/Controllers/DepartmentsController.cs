using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller//todo controlador é uma subclasse de Controller
    {
        public IActionResult Index()
        {
            //criando uma lista do tipo Department generica para teste usando a sintaxe 
            //de instanciação automatica (que é colocar os valores direto dentro de chaves)
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1, Name = "Eletronics" });
            list.Add(new Department { Id = 2, Name = "Fashion" });

            //para enviar dados do Controller para View, neste caso,
            //aqui estamos enviando a nossa lista, colocando diretamente dentro do argumento
            //da função View, que será usado lá na pasta Views
            return View(list);
        }
    }
}
