<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
=======
﻿using System.Collections.Generic;
>>>>>>> origin/teja
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using webappd.Models;
<<<<<<< HEAD
=======

>>>>>>> origin/teja
namespace Webappd.Controllers
{
    public class SpiceDisplayController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        // GET: SpiceDisplay/Index
        public ActionResult Index()
        {
            List<Spices> spice = new List<Spices>();
<<<<<<< HEAD
=======

>>>>>>> origin/teja
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath FROM Spices"; // Query to fetch all spices
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
<<<<<<< HEAD
=======

>>>>>>> origin/teja
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
