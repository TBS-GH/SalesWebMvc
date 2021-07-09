using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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
            //esse comando é só pra garantir que nao apareca o erro da criação de um novo seller
            //colocando ele no primeiro departamento por default, mais pra frente iremos
            //fazer uma alteração para selecionar o departamento que ele vai ser alocado
            //  obj.Department = _context.Department.First();

            //se usarmos apenas o add. não tem como confirmar que o objeto foi inserido
            _context.Add(obj);

            //para verificar se o obj vai ser inserio com sucesso no DB temos que usar o
            //comando .SaveChanges
            _context.SaveChanges();
        }

        //aqui vai retornar um vendendor que possui o id informado, se o vendedor nao existir
        //vai retornar null
        public Seller FindById(int id)
        {
            //essa operação abaixo retorna apenas os dados(neste caso o Id) do vendedor,
            //não é possivel retornar o Departamento dele, para isso devemos fazer:
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);

            //devemos incluir a operação .include() que faz parte do namespace
            //Microsoft.EntityFrameworkCore, ele vai incluir o dado que desejarmos
            //que no caso é o Departamento. Devemos colocalo antes da operação
            //FirstOrDefault
            //essa operação é chamada de EagerLoading: que é já carregar outros objetos associados
            //a aquele obj principal
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            //selecioando o vendedor que sera removido
            var obj = _context.Seller.Find(id);
            //aqui estamos removendo do DBset
            _context.Seller.Remove(obj);
            //aqui o framework tem que efeitivar lá no banco de dados
            _context.SaveChanges();
        }

        //aqui vamos atualizar um obj do tipo Seller
        public void Update(Seller obj)
        {
            //primeiramente vamos verificar se o id desse obje tem no DB
            //o metodo .Any() verifica se existe algum registro no DB com a condição que
            //vc colocou como parametro. Lembrar que o ! significa se não achar o Id fornecido
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            //se passar por esse if, significa que vamos atualiza-lo
            _context.Update(obj);
            //**********OBSERVAÇÃO*********
            //quando vc chama a função de atualizar o DB, o DB pode retornar uma excessão
            //de conflito de concorrencia. Se esse erro ocorrer no DB, o entitie framwork vai
            //produzir uma excessão chamda de DbUpdateConcurrencyException. Por isso devemos 
            //colocar um bloco try
            //*****************************
            //salvando as alterações no DB
            try
            {
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                //se acontecer essa excessão aqui, do entitie framework vamos relançar uma outra
                //excessão em nível de serviço que vai ser a DbConcurrencyException. Isso é muito importante
                //para segregar as camadas. A camada de serviço não vai porpagar excessoes do nivel de 
                //acesso a dados, se ela acontecer a minha camada de serviço vai lançar uma excessão da
                //camada dela e ai o meu controlador(SellersController) ele vai ter que lidar somente
                //com as excessões da camada de serviço. Respeitando a arquitetura MVC
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
