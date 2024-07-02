using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using webappd.Models;

namespace edit.Controllers
{
    public class IndetailedController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: Indetailed
        public ActionResult Index()
        {
            var spicesList = GetAllSpices();
            return View(spicesList);
        }

        private List<CombinedSpices> GetAllSpices()
        {
            List<CombinedSpices> spicesList = new List<CombinedSpices>();

            // Fetch data from Table1
<<<<<<< HEAD
            string query1 = "SELECT ImageName, ImagePath,Description,Price FROM Spices";
            spicesList.AddRange(FetchSpices(query1));

            // Fetch data from Table2
            string query2 = "SELECT ImageName, ImagePath,Description, Price FROM DryFruits";
            spicesList.AddRange(FetchSpices(query2));

            // Fetch data from Table3
            string query3 = "SELECT ImageName, ImagePath , Description , Price FROM Chocolate";
=======
            string query1 = "SELECT ImageName, ImagePath FROM Spices";
            spicesList.AddRange(FetchSpices(query1));

            // Fetch data from Table2
            string query2 = "SELECT ImageName, ImagePath FROM DryFruits";
            spicesList.AddRange(FetchSpices(query2));

            // Fetch data from Table3
            string query3 = "SELECT ImageName, ImagePath FROM Chocolate";
>>>>>>> origin/teja
            spicesList.AddRange(FetchSpices(query3));

            return spicesList;
        }

        private List<CombinedSpices> FetchSpices(string query)
        {
            List<CombinedSpices> spices = new List<CombinedSpices>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CombinedSpices spice = new CombinedSpices
                            {
                                ImageName = reader["ImageName"].ToString(),
<<<<<<< HEAD
                                ImagePath = reader["ImagePath"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (int)Convert.ToDecimal(reader["Price"])
=======
                                ImagePath = reader["ImagePath"].ToString()
>>>>>>> origin/teja
                            };
                            spices.Add(spice);
                        }
                    }
                }
            }

            return spices;
        }
    }
}
