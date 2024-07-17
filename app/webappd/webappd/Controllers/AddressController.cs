using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class AddressController : Controller
    {
        // Action to display the product details
        public ActionResult Product(int id)
        {
            // Simulated product data
            var model = new Spices
            {
                ImageName = "Sample Spice",
                ImagePath = "~/Content/sample-spice.png",
                Price = 100,
                Description = "This is a sample spice."
            };

            return View(model);
        }

        // Action to show the address input page
        public ActionResult AddressInput(decimal amount)
        {
            ViewBag.Amount = amount; // Pass amount to the view
            return View();
        }

        // Action to handle payment
        [HttpPost]
        public ActionResult PaymentPage(decimal amount, string address)
        {
            ViewBag.Amount = amount;
            ViewBag.Address = address; // Store or use as needed
            return View();
        }

        // Action to create an order for Razorpay
        [HttpPost]
        public ActionResult CreateOrder([FromBody] OrderRequest request)
        {
            // Simulated order creation logic
            var orderId = "order_123"; // Simulated order ID
            return Json(new { orderId });
        }

        // Action to handle success
        public ActionResult Success()
        {
            // Implement success logic here
            return View();
        }
    }

}