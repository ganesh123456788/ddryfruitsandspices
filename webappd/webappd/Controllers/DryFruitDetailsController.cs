using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webappd.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;
namespace webappd.Controllers
{
    public class DryFruitDetailsController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        // GET: DryFruitDetails2/Details/{id}
        public ActionResult Details2(string id)
        {
            DryFruits dryfruit = new DryFruits();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM DryFruits WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@ImageName", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dryfruit.ImageName = reader["ImageName"].ToString();
                    dryfruit.ImagePath = reader["ImagePath"].ToString();
                }

                reader.Close();
            }

            return View(dryfruit);
        }
    }
}