using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Configuration;
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
<<<<<<< HEAD
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
=======
    public class DryfruitsDisplayController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: DryFruitsDisplay
        public ActionResult Index()
        {
            List<DryFruits> dryFruitsList = new List<DryFruits>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ImageName, ImagePath FROM DryFruits", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "/Images/default-image.png";

                    DryFruits dryFruits = new DryFruits
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = imagePath
                    };
                    dryFruitsList.Add(dryFruits);
                }
            }
            return View(dryFruitsList);
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
        }
    }
}
