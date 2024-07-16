using System.Web.Mvc;
using webappd.Models; // Ensure this namespace matches your project structure
using Stripe;

namespace webappd.Controllers
{
    public class StripePaymentController : Controller
    {
        public ActionResult Checkout(DryFruits model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Charge(string stripeEmail, string stripeToken, int amount)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = amount * 100, // Amount in cents
                Description = "Sample Charge",
                Currency = "usd",
                Customer = customer.Id
            });

            return View();
        }
    }
}
