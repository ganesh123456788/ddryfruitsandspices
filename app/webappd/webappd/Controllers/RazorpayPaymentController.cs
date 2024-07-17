using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Razorpay.Api;
using webappd.Models;

namespace webappd.Controllers
{
    public class RazorpayPaymentController : Controller
    {
        private readonly string _key = "rzp_test_rOKYyczXYKLutp"; // Replace with your Key ID
        private readonly string _secret = "GPMkUY0spf3gq2A2FfoypGPW"; // Replace with your Key Secret

        public ActionResult Checkout(DryFruits model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOrder(int amount)
        {
            RazorpayClient client = new RazorpayClient(_key, _secret);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", amount * 100); // Amount in paise
            options.Add("currency", "INR");
            options.Add("payment_capture", "1"); // 1 for automatic capture, 0 for manual capture

            Order order = client.Order.Create(options);
            return Json(new { orderId = order["id"].ToString() });
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
