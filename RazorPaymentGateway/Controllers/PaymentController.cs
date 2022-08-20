using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorPaymentGateway.ViewModels;

using Razorpay.Api;

namespace RazorPaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        [HttpPost]
        [Route("Order/Create")]
        public IActionResult postCreateOrder(PaymentDetails details)
        {
            Console.WriteLine(details.CustomerName);
            Console.WriteLine(details.TotalAmount);
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Model not valid" });

            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            // takes (key, secret)
            RazorpayClient client = new RazorpayClient("rzp_test_tpOc2nb7SL0Psb", "JiIysJYeWNUdxnSZ57XwI3At");

            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", details.TotalAmount);
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0");
            //options.Add("notes", "some notes here...");

            Console.WriteLine(options);
            Order order = client.Order.Create(options);

            string orderId = order["id"].ToString();


            return Ok(new {success = "Model is valid", orderId = orderId, customerDetails = details});
        }
    }
}