using System;
using System.Collections.Generic;
using System.Configuration;
<<<<<<< HEAD
using System.Data;
=======
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
using System.Data.SqlClient;
using System.IO;
using System.Web;
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
<<<<<<< HEAD
            var spicesList = new List<Spices>();
=======
            var dryFruitsList = new List<Spices>();
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
<<<<<<< HEAD
                    var spice = new Spices
=======
                    var dryFruits = new Spices
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
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
<<<<<<< HEAD
                        spice.ImagePath = "data:image/png;base64," + base64String;
                    }
                    spicesList.Add(spice);
                }
                connection.Close();
            }
            return View(spicesList);
=======
                        dryFruits.ImagePath = "data:image/png;base64," + base64String;
                    }
                    dryFruitsList.Add(dryFruits);
                }
                connection.Close();
            }
            return View(dryFruitsList);
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
        }

        // GET: SpicesEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpicesEdit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public ActionResult Create(Spices spice)
=======
        public ActionResult Create(Spices dryFruits)
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
<<<<<<< HEAD
                if (spice.ImageFile != null)
                {
                    using (var binaryReader = new BinaryReader(spice.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(spice.ImageFile.ContentLength);
=======
                if (dryFruits.ImageFile != null)
                {
                    using (var binaryReader = new BinaryReader(dryFruits.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(dryFruits.ImageFile.ContentLength);
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Spices (ImageFile, ImageName, ImagePath, Description, Price) VALUES (@ImageFile, @ImageName, @ImagePath, @Description, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);
<<<<<<< HEAD
                    command.Parameters.Add("@ImageFile", SqlDbType.VarBinary).Value = (object)imageData ?? DBNull.Value;
                    command.Parameters.AddWithValue("@ImageName", spice.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", spice.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", spice.Description);
                    command.Parameters.AddWithValue("@Price", spice.Price);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ModelState.AddModelError("", "Error while creating spice: " + ex.Message);
                        return View(spice);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(spice);
        }

        // GET: SpiceDisplay/Edit/5
        public ActionResult Edit(string imageName)
        {
            Spice spice = GetSpiceByName(Name);
            if (spice == null)
            {
                return HttpNotFound();
            }
            return View(spice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Spice spice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (spice.ImageFile != null)
                    {
                        string imagePath = SaveImage(spice.ImageFile);
                        spice.ImagePath = imagePath;
                    }

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Spices SET Name = @Name, Type = @Type, Price = @Price, ImagePath = @ImagePath WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Id", spice.Id);
                            cmd.Parameters.AddWithValue("@Name", spice.Name);
                            cmd.Parameters.AddWithValue("@Type", spice.Type);
                            cmd.Parameters.AddWithValue("@Price", spice.Price);
                            cmd.Parameters.AddWithValue("@ImagePath", spice.ImagePath); // Assuming ImagePath is the column name in your database
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle exception, log it, or display an error message
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                    // Optionally: return a specific view or message indicating the error
                }
            }
            return View(spice);
        }



        // GET: SpicesEdit/Delete
        public ActionResult Delete(string imageName)
        {
            var spice = GetSpiceByImageName(imageName);
            return View(spice);
=======
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(dryFruits);
        }

        // GET: SpicesEdit/Edit
        public ActionResult Edit(string imageName)
        {
            Spices dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dryFruits = new Spices
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
                        dryFruits.ImagePath = "data:image/png;base64," + base64String;
                    }
                }
                connection.Close();
            }
            return View(dryFruits);
        }

        // POST: SpicesEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Spices dryFruits)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (dryFruits.ImageFile != null)
                {
                    using (var binaryReader = new BinaryReader(dryFruits.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(dryFruits.ImageFile.ContentLength);
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Spices SET ImageFile = @ImageFile, ImagePath = @ImagePath, Description = @Description, Price = @Price WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(dryFruits);
        }

        // GET: SpicesEdit/Delete
        public ActionResult Delete(string imageName)
        {
            Spices dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dryFruits = new Spices
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
                        dryFruits.ImagePath = "data:image/png;base64," + base64String;
                    }
                }
                connection.Close();
            }
            return View(dryFruits);
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
        }

        // POST: SpicesEdit/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Spices WHERE ImageName = @ImageName";
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
                    ModelState.AddModelError("", "Error while deleting spice: " + ex.Message);
                    var spice = GetSpiceByImageName(imageName);
                    return View(spice);
                }
                finally
                {
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }

        private Spices GetSpiceByImageName(string imageName)
        {
            Spices spice = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spice = new Spices
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
                        spice.ImagePath = "data:image/png;base64," + base64String;
                    }
                }
                connection.Close();
            }
            return spice;
        }
=======
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
>>>>>>> 4d609a7fb8a5ad14181800e5b77cd95aaf2b7aef
    }
}
