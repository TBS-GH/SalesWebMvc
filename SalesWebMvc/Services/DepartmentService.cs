using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    //criando um serviço de departamentos, criando um metodo que retorna os departamentos ordenados
    public class DepartmentService
    {
        //adicioando uma dependencia do DB services
        //nos adicionamos o comando readonly para previnir que essa dependencia não possa ser alterada
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //criando um método para retornar todos os departamentos
        public List<Department> FindAll()
        {
            //poderiamos fazer dessa forma que também da certo
            //return _context.Department.ToList();

            //aqui estamos utilizando um recurso do link para mostrar ou ordenar apenas 
            //os nomes dos departamentos com o metodo OrderBy()
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
