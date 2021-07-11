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

        [Required(ErrorMessage = "{0} required")] //define que o campo é obrigatório
        //definindo limites para o tamanho do nome, como max e min
        //{0} é a propriedade Name; {1} é o primeiro parametro indicado (60); {2} é o segundo parametro (3)
        //explicitamente ficaria assim: ErrorMessage = "Name size should be between 3 and 60 characters"
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1} characters")]
        public string Name { get; set; }
        //************************************

        [Required(ErrorMessage = "{0} required")] //define que o campo é obrigatório
        [EmailAddress(ErrorMessage = "Enter a valid email")] //deifne endereço de email valido
        //podemos usar o datatype para email tb, que vai ficar um link pra enviar email direto pra pessoa
        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //************************************
        
        [Required(ErrorMessage = "{0} required")] //define que o campo é obrigatório
        //vamos definir aqui um custom lable para o BirthDate, para isso vamos usar a anotação
        //[Display] a string que tiver dentro "" é que vai aparecer na tela
        [Display(Name = "Birth Date")] //é assim q vc customiza o que vai aparecer no display
        //agora vamos reitar as horas e minutos da data de nascimento com a tag [DataType]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        //************************************

        [Required(ErrorMessage = "{0} required")] //define que o campo é obrigatório
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] //min e max salario        
        [Display(Name = "Base Salary")] //aqui mostra o nome de forma correta
        //aqui o valor do salario é apresentado com duas casas decimais. o zero dentro das chaves
        //é pra indicar o valor do atributo, já o f2 é a quantidade de casas decimais. pra moeda
        //basta colocar o :c2
        [DisplayFormat(DataFormatString = "{0:c2}")]
        public double BaseSalary { get; set; }
        //************************************

        public Department Department { get; set; }
        //aqui estamos avisando o entitie do framework pra ele garantir que esse id vai ter que existir,
        //uma vez que o int não pode ser nulo
        public int DepartmentId { get; set; } 
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //aqui já estamos instanciando a lista

        //construtores
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
