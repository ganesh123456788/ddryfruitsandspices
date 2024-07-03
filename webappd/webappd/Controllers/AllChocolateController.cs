using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class AllChocolateController : Controller
    {
        // Replace with your connection string from Web.config or App.config
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // Define constants for paths
        private const string ContentFolderPath = "~/Content";

        // GET: AllChocolate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllChocolate/Create
        [HttpPost]
        public ActionResult Create(Chocolate model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if an image file is uploaded
                    if (model.ImageFile != null && model.ImageFile.ContentLength > 0 && !string.IsNullOrEmpty(model.ImageName))
                    {
                        // Save the uploaded file to a folder, for example "Images"
                        string fileName = Path.GetFileName(model.ImageFile.FileName);
                        string path = Path.Combine(Server.MapPath(ContentFolderPath), fileName);
                        if (!Directory.Exists(Server.MapPath(ContentFolderPath)))
                        {
                            Directory.CreateDirectory(Server.MapPath(ContentFolderPath));
                        }
                        model.ImageFile.SaveAs(path);
                        model.ImagePath = Url.Content(Path.Combine(ContentFolderPath, fileName));
                        SaveImageToDatabase(model.ImageName, model.ImagePath, model.Description, model.Price);

                        // Optionally, redirect to a success page or show a success message
                        return RedirectToAction("Success");
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

        private void SaveImageToDatabase(string imageName, string imagePath, string Description, decimal Price)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ImageName", imageName);
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // GET: AllChocolate/Success
        public ActionResult Success()
        {
            return View();
        }
    }
}
