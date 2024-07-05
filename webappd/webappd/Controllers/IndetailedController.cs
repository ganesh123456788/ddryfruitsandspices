using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class IndetailedController : Controller
    {
        // GET: Indetailed/Index
        public ActionResult Index()
        {
            List<Chocolate> chocolates = new List<Chocolate>();
            List<DryFruits> dryFruits = new List<DryFruits>();
            List<CombinedSpices> combinedSpices = new List<CombinedSpices>();

            string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

            // Fetch Chocolates
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price FROM Chocolates";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    chocolates.Add(new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price"))
                    });
                }
                reader.Close();
            }

            // Fetch Dry Fruits
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price FROM DryFruits";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dryFruits.Add(new DryFruits
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price"))
                    });
                }
                reader.Close();
            }

            // Fetch Combined Spices
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price FROM Spices";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    combinedSpices.Add(new CombinedSpices
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price"))
                    });
                }
                reader.Close();
            }

            // Pass data to view
            ViewBag.Chocolates = chocolates;
            ViewBag.DryFruits = dryFruits;
            ViewBag.CombinedSpices = combinedSpices;

            return View();
        }
    }
}
