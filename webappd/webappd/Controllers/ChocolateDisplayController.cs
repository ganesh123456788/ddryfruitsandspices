using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class ChocolateDisplayController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: ChocolateDisplay/Index
        public ActionResult Index()
        {
            List<Chocolate> chocolates = new List<Chocolate>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM Chocolate"; // Query to fetch all spices
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
<<<<<<< HEAD
                    Chocolate chocolate = new Chocolate();  
=======
                    Chocolate chocolate = new Chocolate();
>>>>>>> origin/teja
                    chocolate.ImageName = reader["ImageName"].ToString();
                    chocolate.ImagePath = reader["ImagePath"].ToString();
                    chocolates.Add(chocolate);
                }

                reader.Close();
            }

            return View(chocolates);
        }
    }
}