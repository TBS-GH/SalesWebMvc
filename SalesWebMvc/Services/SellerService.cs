using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        //adicioando uma dependencia do DB services
        //nos adicionamos o comando readonly para previnir que essa dependencia não possa ser alterada
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            //aqui vai acessar a minha fonte dados relacionada a tabela de vendedores
            //e converter para uma lista
            //por enquanto esta operação é assincrona, ou seja, ele vai rodar esse acesso ao banco de dados
            //no comando abaixo e a aplicação vai ficar bloqueada esperando esta operação terminar.
            return _context.Seller.ToList();
            //mai pra frente vamos transformar em operação mais otimizadas AGUARDEEEMMMMM
        }

        //inserindo um novo vendendor no DB
        public void Insert(Seller obj)
        {
            //se usarmos apenas o add. não tem como confirmar que o objeto foi inserido
            _context.Add(obj);

            //para verificar se o obj vai ser inserio com sucesso no DB temos que usar o
            //comando .SaveChanges
            _context.SaveChanges();
        }
    }
}
