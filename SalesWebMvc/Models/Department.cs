using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    //aqui é uma classe normal dentro da pasta models
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); //aqui já estamos instanciando a lista

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //adicionando vendedores na lista
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        //total de vendas do departamento em um intervalo de datas
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
