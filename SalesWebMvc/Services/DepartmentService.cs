using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

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

        //******************** Metodo Asynchronous ************************************
        public async Task<List<Department>> FindAllAsync()
        {
            //poderiamos fazer dessa forma que também da certo
            //return _context.Department.ToList();

            //aqui estamos utilizando um recurso do link para mostrar ou ordenar apenas 
            //os nomes dos departamentos com o metodo OrderBy()
            //a palavra await fala pro compilador que a chamada deve ser retornada de forma
            //assincrona, dessa forma a minha execução não vai bloquear a minha aplicação
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }


        //******************** Metodo Sincrono ************************************
        //criando um método para retornar todos os departamentos de forma Sincrona
        //ou seja, o programa perde muita eficiencia porque ele espera essa função
        //ser totalmente executada e a execução total fica a espera dela, logo aumenta e muito
        //tempo de execução
        /*public List<Department> FindAll()
        {
            //poderiamos fazer dessa forma que também da certo
            //return _context.Department.ToList();

            //aqui estamos utilizando um recurso do link para mostrar ou ordenar apenas 
            //os nomes dos departamentos com o metodo OrderBy()
            return _context.Department.OrderBy(x => x.Name).ToList();
        }*/
    }
}
