using ClientsInvoice.Models;
using System.ComponentModel.DataAnnotations;

namespace ClientsInvoice.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Must be greater than 3 characters")]
        [MaxLength(70, ErrorMessage = "Must be less than 70 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MinLength(10, ErrorMessage = "Must be greater than 10 characters")]
        [MaxLength(150, ErrorMessage = "Must be less than 150 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Is Active is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Customer type is required")]
        public int CostumerTypeId { get; set; }

        public void MapProperties(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Address = customer.Address;
            IsActive = customer.IsActive;
            CostumerTypeId = customer.CostumerTypeId;
        }
    }
}