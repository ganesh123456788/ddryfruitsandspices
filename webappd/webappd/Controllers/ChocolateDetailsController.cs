using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
<<<<<<< HEAD
using webappd.Models;
=======
using System.Web.Mvc;
using webappd.Models;

>>>>>>> origin/teja
namespace webappd.Controllers
{
    public class ChocolateDetailsController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
<<<<<<< HEAD
=======

>>>>>>> origin/teja
        // GET: ChocolateDetails/Details1//{id}
        public ActionResult Details1(string id)
        {
            Chocolate Chocolates = new Chocolate();
<<<<<<< HEAD
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
=======

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM Chocolate WHERE ImageName = @ImageName";
>>>>>>> origin/teja
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@ImageName", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
<<<<<<< HEAD
=======

>>>>>>> origin/teja
                if (reader.Read())
                {
                    Chocolates.ImageName = reader["ImageName"].ToString();
                    Chocolates.ImagePath = reader["ImagePath"].ToString();
<<<<<<< HEAD
                    Chocolates.Description = reader["Description"].ToString();
                    Chocolates.Price = (int)Convert.ToDecimal(reader["Price"]);
=======
>>>>>>> origin/teja
                }

                reader.Close();
            }

            return View(Chocolates);
        }

    }
}