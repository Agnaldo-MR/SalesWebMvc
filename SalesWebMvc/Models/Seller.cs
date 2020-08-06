using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        [Display(Name = "Código")] // Máscara do Rótulo da tabela        
        public int Id { get; set; }
                
        [Required(ErrorMessage = "{0} required (Nome obrigatório)")] // {0} = Atributo Name
        [Display(Name = "Nome")] // Máscara do Rótulo da tabela
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1} (Nr de Caracteres entre 3 e 60)")]
        // Número de caracteres:  máximo e mínimo. {0} = Atributo Name - {2} = 3 do MinimumLength - {1} = 60 do StringLength
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required (Email obrigatório)")] // {0} = Atributo Email
        [EmailAddress(ErrorMessage ="Enter a valid email (Entre com um e-mail válido)")] // Para validar o e-mail
        [DataType(DataType.EmailAddress)] // Para colocar o e-mail como link
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required (Data obrigatória)")] // {0} = Atributo Data de Nascimento
        [Display(Name = "Data de Nascimento")] // Máscara do Rótulo da tabela
        [DataType(DataType.Date)] // Para retirar as horas e minutos
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // Formatar a Data
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required (Salário obrigatório)")] // {0} = Atributo salário Base
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] // Para validar salário entre {1}100.0 e {2}50000.0
        [Display(Name = "Salário Base")] // Máscara do Rótulo da tabela
        [DisplayFormat(DataFormatString = "{0:F2}")] // Formatar valor com 2 casas decimais
        public double BaseSalary { get; set; }
                
        public Department Department { get; set; } // Associação cada "Seller" possui um "Department"

        [Display(Name = "Departamento")] // Máscara do Rótulo da tabela
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); // Associação de um "Seller" para vários "SalesRecord"

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

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount); // Retorna a venda "Amount" nas datas específicas
        }
    }
}