using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using webappd.Models;

namespace AllSpicesController.Controllers
{
    public class ImageController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: Image/Upload
        public ActionResult Upload()
        {
            return View();
        }

        // POST: Image/Upload
        [HttpPost]
        public ActionResult Upload(Spices model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ImageFile != null && model.ImageFile.ContentLength > 0 && !string.IsNullOrEmpty(model.ImageName))
                    {
                        string fileName = Path.GetFileName(model.ImageFile.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content"), fileName);
<<<<<<< HEAD
=======

>>>>>>> origin/teja
                        if (!Directory.Exists(Server.MapPath("~/Content")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content"));
                        }
<<<<<<< HEAD
                        model.ImageFile.SaveAs(path);
                        model.ImagePath = Url.Content(Path.Combine("~/Content/", fileName));
                        // Save image details to database
                        SaveImageToDatabase(model.ImageName, model.ImagePath,model.Description,model.Price);
=======

                        model.ImageFile.SaveAs(path);
                        model.ImagePath = Url.Content(Path.Combine("~/Content/", fileName));

                        // Save image details to database
                        SaveImageToDatabase(model.ImageName, model.ImagePath);

>>>>>>> origin/teja
                        // Optionally, redirect to a success page or show a success message
                        return RedirectToAction("UploadSuccess");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Please select an image and enter a name.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error uploading image: " + ex.Message;
                }
            }

            // If the upload fails or validation fails, return to the upload page with errors
            return View(model);
        }

<<<<<<< HEAD
        private void SaveImageToDatabase(string imageName, string imagePath, string Description, decimal Price)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Spices (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
=======
        private void SaveImageToDatabase(string imageName, string imagePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Spices (ImageName, ImagePath) VALUES (@ImageName, @ImagePath)";
>>>>>>> origin/teja
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ImageName", imageName);
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);
<<<<<<< HEAD
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Price", Price);
=======
>>>>>>> origin/teja
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // GET: Image/UploadSuccess
        public ActionResult UploadSuccess()
        {
            return View();
        }
    }
}
