using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class ChocolateDisplayController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            try
            {
                var chocolates = GetChocolatesFromDatabase();
                return View(chocolates);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        private List<Chocolate> GetChocolatesFromDatabase()
        {
            var chocolates = new List<Chocolate>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath FROM Chocolate";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    chocolates.Add(new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"].ToString()
                    });
                }
            }

            return chocolates;
        }
    }
}
