using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
<<<<<<< HEAD
=======
using System.Linq;
using System.Net;
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
using System.Web;
using System.Web.Mvc;
using webappd.Models;

<<<<<<< HEAD
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
=======
namespace Crudapp.Controllers
{
    public class ChocolateEditController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: ChocolateEdit
        public ActionResult Index()
        {
            var chocolates = GetChocolates();
            return View(chocolates);
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
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
<<<<<<< HEAD
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
=======
                // Handle ImageFile upload
                if (chocolate.ImageFile != null && chocolate.ImageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(chocolate.ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    chocolate.ImageFile.SaveAs(path);
                    chocolate.ImagePath = "~/Content/" + fileName;
                }
                else
                {
                    // Set default ImagePath if no file uploaded
                    chocolate.ImagePath = "~/Content/default-chocolate.png"; // Default image path
                }

                InsertChocolate(chocolate);
                return RedirectToAction("Index");
            }

            return View(chocolate);
        }

        // GET: ChocolateEdit/Edit/5
        public ActionResult Edit(string imageName)
        {
            if (imageName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chocolate = GetChocolateByImageName(imageName);
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
            if (chocolate == null)
            {
                return HttpNotFound();
            }
<<<<<<< HEAD
            return View(chocolate);
        }

        // POST: ChocolateEdit/Edit
=======

            return View(chocolate);
        }

        // POST: ChocolateEdit/Edit/5
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Chocolate chocolate)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
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
=======
                // Handle ImageFile upload
                if (chocolate.ImageFile != null && chocolate.ImageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(chocolate.ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    chocolate.ImageFile.SaveAs(path);
                    chocolate.ImagePath = "~/Content/" + fileName;
                }

                UpdateChocolate(chocolate);
                return RedirectToAction("Index");
            }

            return View(chocolate);
        }

        // GET: ChocolateEdit/Delete/5
        public ActionResult Delete(string imageName)
        {
            if (imageName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chocolate = GetChocolateByImageName(imageName);
            if (chocolate == null)
            {
                return HttpNotFound();
            }

            return View(chocolate);
        }

        // POST: ChocolateEdit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
        {
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
            var chocolate = GetChocolateByImageName(imageName);
            if (chocolate == null)
            {
                return HttpNotFound();
            }
<<<<<<< HEAD
            return View(chocolate);
        }

        // POST: ChocolateEdit/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
=======

            DeleteChocolate(chocolate.ImageName);
            return RedirectToAction("Index");
        }

        private List<Chocolate> GetChocolates()
        {
            List<Chocolate> chocolates = new List<Chocolate>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Chocolate";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    chocolates.Add(new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (int)Convert.ToDecimal(reader["Price"])
                    });
                }

                reader.Close();
            }

            return chocolates;
        }

        private Chocolate GetChocolateByImageName(string imageName)
        {
            Chocolate chocolate = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Chocolate WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    chocolate = new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (int)Convert.ToDecimal(reader["Price"])
                    };
                }

                reader.Close();
            }

            return chocolate;
        }

        private void InsertChocolate(Chocolate chocolate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price) " +
                               "VALUES (@ImageName, @ImagePath, @Description, @Price)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath);
                command.Parameters.AddWithValue("@Description", chocolate.Description);
                command.Parameters.AddWithValue("@Price", chocolate.Price);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void UpdateChocolate(Chocolate chocolate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Chocolate " +
                               "SET ImagePath = @ImagePath, Description = @Description, Price = @Price " +
                               "WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath);
                command.Parameters.AddWithValue("@Description", chocolate.Description);
                command.Parameters.AddWithValue("@Price", chocolate.Price);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void DeleteChocolate(string imageName)
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Chocolate WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

<<<<<<< HEAD
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
=======
                connection.Open();
                command.ExecuteNonQuery();
            }
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
        }
    }
}
