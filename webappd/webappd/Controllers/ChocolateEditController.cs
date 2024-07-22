using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class ChocolateEditController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ChocolatesDBConnectionString"].ConnectionString;

        // GET: ChocolateEdit/Index
        public ActionResult Index()
        {
            var chocolateList = new List<Chocolate>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Chocolate";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var chocolate = new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"]?.ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"])
                    };
                    if (reader["ImageFile"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])reader["ImageFile"];
                        string base64String = Convert.ToBase64String(imageData);
                        chocolate.ImagePath = "data:image/png;base64," + base64String;
                    }
                    chocolateList.Add(chocolate);
                }
                connection.Close();
            }
            return View(chocolateList);
        }

        // GET: ChocolateEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChocolateEdit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Chocolate chocolate)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (chocolate.ImageFile != null)
                {
                    using (var binaryReader = new BinaryReader(chocolate.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(chocolate.ImageFile.ContentLength);
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Chocolate (ImageFile, ImageName, ImagePath, Description, Price) VALUES (@ImageFile, @ImageName, @ImagePath, @Description, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@ImageFile", SqlDbType.VarBinary).Value = (object)imageData ?? DBNull.Value;
                    command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", chocolate.Description);
                    command.Parameters.AddWithValue("@Price", chocolate.Price);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ModelState.AddModelError("", "Error while creating chocolate: " + ex.Message);
                        return View(chocolate);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(chocolate);
        }

        // GET: ChocolateEdit/Edit
        public ActionResult Edit(string imageName)
        {
            Chocolate chocolate = GetChocolateByImageName(imageName);
            if (chocolate == null)
            {
                return HttpNotFound();
            }
            return View(chocolate);
        }

        // POST: ChocolateEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Chocolate chocolate)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (chocolate.ImageFile != null)
                {
                    using (var binaryReader = new BinaryReader(chocolate.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(chocolate.ImageFile.ContentLength);
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Chocolate SET ImageFile = @ImageFile, ImageName = @ImageName, ImagePath = @ImagePath, Description = @Description, Price = @Price WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@ImageFile", SqlDbType.VarBinary).Value = (object)imageData ?? DBNull.Value;
                    command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", chocolate.Description);
                    command.Parameters.AddWithValue("@Price", chocolate.Price);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ModelState.AddModelError("", "Error while updating chocolate: " + ex.Message);
                        return View(chocolate);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(chocolate);
        }

        // GET: ChocolateEdit/Delete
        public ActionResult Delete(string imageName)
        {
            var chocolate = GetChocolateByImageName(imageName);
            if (chocolate == null)
            {
                return HttpNotFound();
            }
            return View(chocolate);
        }

        // POST: ChocolateEdit/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Chocolate WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    ModelState.AddModelError("", "Error while deleting chocolate: " + ex.Message);
                    var chocolate = GetChocolateByImageName(imageName);
                    return View(chocolate);
                }
                finally
                {
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }

        private Chocolate GetChocolateByImageName(string imageName)
        {
            Chocolate chocolate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    chocolate = new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"]?.ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"])
                    };
                    if (reader["ImageFile"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])reader["ImageFile"];
                        string base64String = Convert.ToBase64String(imageData);
                        chocolate.ImagePath = "data:image/png;base64," + base64String;
                    }
                }
                connection.Close();
            }
            return chocolate;
        }
    }
}
