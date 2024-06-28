using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class ChocolateDetailsController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: ChocolateDetails/Details1//{id}
        public ActionResult Details1(string id)
        {
            Chocolate Chocolates = new Chocolate();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM Chocolate WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@ImageName", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Chocolates.ImageName = reader["ImageName"].ToString();
                    Chocolates.ImagePath = reader["ImagePath"].ToString();
                }

                reader.Close();
            }

            return View(Chocolates);
        }

    }
}