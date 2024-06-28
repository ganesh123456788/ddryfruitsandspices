using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class SpicesEditController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: SpiceEdit/Index
        public ActionResult Index()
        {
            List<Spices> spicesList = new List<Spices>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath FROM Spices";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Spices spice = new Spices
                        {
                            ImageName = reader["ImageName"].ToString(),
                            ImagePath = reader["ImagePath"].ToString()
                        };
                        spicesList.Add(spice);
                    }
                    con.Close();
                }
            }

            return View(spicesList);
        }

        // POST: Home/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string imageName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Spices WHERE ImageName = @ImageName";
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
