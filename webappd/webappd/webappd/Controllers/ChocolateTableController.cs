﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using webappd.Models;
using System.Diagnostics;

namespace webappd.Controllers
{
    public class ChocolateTableController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: ChocolateTable/Index
        public ActionResult Index()
        {
            List<Chocolate> Chocolates = new List<Chocolate>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ImageName, ImagePath, Descrition, Price FROM Chocolate"; // Query to fetch all spices
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Chocolate chocolate = new Chocolate();
                    chocolate.ImageName = reader["ImageName"].ToString();
                    chocolate.ImagePath = reader["ImagePath"].ToString();
                    chocolate.Description = reader["Description"].ToString();
                    chocolate.Price = (int)Convert.ToDecimal(reader["Price"]);

                    Chocolates.Add(chocolate);
                }

                reader.Close();
            }

            return View(Chocolates);
        }
    }
}