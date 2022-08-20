using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPaymentGateway.ViewModels
{
    public class PaymentDetails
    {
        public PaymentDetails()
        {
        }

        [Required(ErrorMessage = "Customer Name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter valid Amount")]
        public double TotalAmount { get; set; }

        [Required(ErrorMessage = "Receipt Number is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Receipt Number")]
        public int ReceiptNumber { get; set; }

        public string Address { get; set; }
    }
}

