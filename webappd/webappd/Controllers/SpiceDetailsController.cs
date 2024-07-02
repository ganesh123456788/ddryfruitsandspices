<<<<<<< HEAD
﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;
=======
﻿using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

>>>>>>> origin/teja
namespace webappd.Controllers
{
    public class SpiceDetailsController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
<<<<<<< HEAD
=======

>>>>>>> origin/teja
        // GET: SpiceDetails/Details/{id}
        public ActionResult Details(string id)
        {
            Spices spice = new Spices();
<<<<<<< HEAD
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
=======

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM Spices WHERE ImageName = @ImageName";
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
                    spice.ImageName = reader["ImageName"].ToString();
                    spice.ImagePath = reader["ImagePath"].ToString();
<<<<<<< HEAD
                    spice.Description = reader["Description"].ToString();
                    spice.Price = (int)Convert.ToDecimal(reader["Price"]);
                }
                reader.Close();
            }
=======
                }

                reader.Close();
            }

>>>>>>> origin/teja
            return View(spice);
        }
    }
}
