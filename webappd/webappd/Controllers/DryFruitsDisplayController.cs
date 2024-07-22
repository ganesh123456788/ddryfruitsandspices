using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class DryFruitsDisplayController : Controller
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            try
            {
                var dryFruits = GetDryFruitsFromDatabase();
                return View(dryFruits);
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                ViewBag.ErrorMessage = "An error occurred while fetching data: " + ex.Message;
                return View("Error");
            }
        }

        private List<DryFruits> GetDryFruitsFromDatabase()
        {
            var dryFruits = new List<DryFruits>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath FROM DryFruits";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dryFruits.Add(new DryFruits
                            {
                                ImageName = reader["ImageName"].ToString(),
                                ImagePath = reader["ImagePath"].ToString()
                            });
                        }
                    }
                }
            }

            return dryFruits;
        }
    }
}
