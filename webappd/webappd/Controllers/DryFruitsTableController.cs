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
    public class DryFruitsTableController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        // GET: DryFruitsTable/Index
        public ActionResult Index()
        {
            List<DryFruits> DryFruit = new List<DryFruits>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM DryFruits"; // Query to fetch all spices
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DryFruits dryfruits = new DryFruits();
                    dryfruits.ImageName = reader["ImageName"].ToString();
                    dryfruits.ImagePath = reader["ImagePath"].ToString();
                    DryFruit.Add(dryfruits);
                }

                reader.Close();
            }

            return View(DryFruit);
        }
    }
}