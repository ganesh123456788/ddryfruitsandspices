using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace webappd.Controllers
{
    public class PaymentController : Controller
    {
        [HttpPost]
        public ActionResult PayWithUPI(string amount)
        {
            var upiId = "9966301597-2@ibl"; // Replace with your personal UPI ID
            var transactionId = Guid.NewGuid().ToString(); // Unique transaction ID

            // Construct the UPI payment URL
            var url = $"upi://pay?pa={upiId}&pn=Teja Lavu&tid={transactionId}&am={amount}&tn=ProductDescription&cu=INR";

            // Log the URL for debugging
            Debug.WriteLine($"Generated UPI URL: {url}");

            // Pass the URL to the view for display
            return View((object)url);
        }
    }
}
