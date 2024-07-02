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
    public class DryFruitsDisplayController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        // GET: DryFruitsDisplay/Index
        public ActionResult Index()
        {
            List<DryFruits> dryfruits = new List<DryFruits>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM DryFruits"; // Query to fetch all spices
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DryFruits dryfruit = new DryFruits();
                    dryfruit.ImageName = reader["ImageName"].ToString();
                    dryfruit.ImagePath = reader["ImagePath"].ToString();
                    dryfruits.Add(dryfruit);
                }

                reader.Close();
            }

            return View(dryfruits);
        }
    }
}