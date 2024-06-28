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
    public class ChocolateEditController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        // GET: ChocolateEdit
        public ActionResult Index()
        {
            List<Chocolate> ChocolateList = new List<Chocolate>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath FROM Chocolate";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Chocolate chocolates = new Chocolate
                        {
                            ImageName = reader["ImageName"].ToString(),
                            ImagePath = reader["ImagePath"].ToString()
                        };
                        ChocolateList.Add(chocolates);
                    }
                    con.Close();
                }
            }

            return View(ChocolateList);
        }

        // POST: Home/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string imageName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Chocolate WHERE ImageName = @ImageName";
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
