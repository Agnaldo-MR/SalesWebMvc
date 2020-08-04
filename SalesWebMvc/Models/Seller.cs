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

        [Display(Name = "Nome")] // Máscara do Rótulo da tabela
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)] // Para colocar o e-mail como link
        public string Email { get; set; }
        
        [Display(Name = "Data de Nascimento")] // Máscara do Rótulo da tabela
        [DataType(DataType.Date)] // Para retirar as horas e minutos
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // Formatar a Data
        public DateTime BirthDate { get; set; }

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