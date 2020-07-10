using System.Collections.Generic;


namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; } // Dados do vendedor
        public ICollection<Department> Departments { get; set; }

    }
}