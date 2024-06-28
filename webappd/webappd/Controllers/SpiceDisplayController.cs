using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using webappd.Models;

namespace Webappd.Controllers
{
    public class SpiceDisplayController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        // GET: SpiceDisplay/Index
        public ActionResult Index()
        {
            List<Spices> spice = new List<Spices>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM Spices"; // Query to fetch all spices
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Spices Spice = new Spices();
                    Spice.ImageName = reader["ImageName"].ToString();
                    Spice.ImagePath = reader["ImagePath"].ToString();
                    spice.Add(Spice);
                }

                reader.Close();
            }

            return View(spice);
        }


    }
}
