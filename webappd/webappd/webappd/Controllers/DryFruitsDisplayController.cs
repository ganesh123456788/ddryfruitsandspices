using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class DryfruitsDisplayController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: DryFruitsDisplay
        public ActionResult Index()
        {
            List<DryFruits> dryFruitsList = new List<DryFruits>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ImageName, ImagePath FROM DryFruits", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "/Images/default-image.png";

                    DryFruits dryFruits = new DryFruits
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = imagePath
                    };
                    dryFruitsList.Add(dryFruits);
                }
            }
            return View(dryFruitsList);
        }
    }
}
