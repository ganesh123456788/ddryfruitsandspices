using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class DryFruitEditController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        // GET: DryFruitEdit
        public ActionResult Index()
        {
            List<DryFruits> DryFruitList = new List<DryFruits>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath FROM DryFruits";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DryFruits chocolates = new DryFruits
                        {
                            ImageName = reader["ImageName"].ToString(),
                            ImagePath = reader["ImagePath"].ToString()
                        };
                        DryFruitList.Add(chocolates);
                    }
                    con.Close();
                }
            }

            return View(DryFruitList);
        }

        // POST: Home/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string imageName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM DryFruits WHERE ImageName = @ImageName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ImageName", imageName);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
