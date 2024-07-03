using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;
namespace webappd.Controllers
{
    public class SpiceDetailsController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        // GET: SpiceDetails/Details/{id}
        public ActionResult Details(string id)
        {
            Spices spice = new Spices();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@ImageName", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spice.ImageName = reader["ImageName"].ToString();
                    spice.ImagePath = reader["ImagePath"].ToString();
                    spice.Description = reader["Description"].ToString();
                    spice.Price = (int)Convert.ToDecimal(reader["Price"]);
                }
                reader.Close();
            }
            return View(spice);
        }
    }
}
