using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //podemos usar o datatype para email tb, que vai ficar um link pra enviar email direto pra pessoa
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //************************************
        //vamos definir aqui um custom lable para o BirthDate, para isso vamos usar a anotação
        //[Display] a string que tiver dentro "" é que vai aparecer na tela
        [Display(Name = "Birth Date")] //é assim q vc customiza o que vai aparecer no display
        //agora vamos reitar as horas e minutos da data de nascimento com a tag [DataType]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        //************************************
        //aqui mostra o nome de forma correta
        [Display(Name = "Base Salary")]
        //aqui o valor do salario é apresentado com duas casas decimais. o zero dentro das chaves
        //é pra indicar o valor do atributo, já o f2 é a quantidade de casas decimais. pra moeda
        //basta colocar o :c2
        [DisplayFormat(DataFormatString = "{0:c2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        //aqui estamos avisando o entitie do framework pra ele garantir que esse id vai ter que existir,
        //uma vez que o int não pode ser nulo
        public int DepartmentId { get; set; } 
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //aqui já estamos instanciando a lista

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //adicionando uma venda
        public void AddSales (SalesRecord sr)
        {
            Sales.Add(sr);
        }

        //removendo uma venda
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        //total de vendas em um determinado periodo de um vendedor qualquer
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
