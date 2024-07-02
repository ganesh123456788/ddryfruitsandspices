using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class SpicesTableController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: SpicesTable/Index
        public ActionResult Index()
        {
            List<Spices> spices = new List<Spices>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM Spices"; // Query to fetch all spices
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Spices spice = new Spices();
                    spice.ImageName = reader["ImageName"].ToString();
                    spice.ImagePath = reader["ImagePath"].ToString();
                    spices.Add(spice);
                }

                reader.Close();
            }

            return View(spices);
        }
    }
}
