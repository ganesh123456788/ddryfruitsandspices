﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webappd.Models;

namespace webappd.Controllers
{
    public class EditController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: Edit
        public ActionResult Index()
        {
            List<Spices> spices = new List<Spices>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Spices";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        spices.Add(new Spices
                        {
                            ImageName = reader["ImageName"].ToString(),
                            ImagePath = reader["ImagePath"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (int)Convert.ToDecimal(reader["Price"])
                        });
                    }
                }
            }
            return View(spices);
        }

        // GET: Edit/Edit/5
        public ActionResult Edit(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Spices spice = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Spices WHERE ImageName = @ImageName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ImageName", imageName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        spice = new Spices
                        {
                            ImageName = reader["ImageName"].ToString(),
                            ImagePath = reader["ImagePath"].ToString()
                        };
                    }
                }
            }

            if (spice == null)
            {
                return HttpNotFound();
            }

            ViewBag.ReturnUrl = Request.UrlReferrer?.ToString();
            return View(spice);
        }

        // POST: Edit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Spices model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    // Save the file
                    var fileName = Path.GetFileName(model.ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    model.ImageFile.SaveAs(path);

                    // Update model properties
                    model.ImageName = fileName;
                    model.ImagePath = "~/Content/" + fileName;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE Spices SET ImagePath = @ImagePath WHERE ImageName = @ImageName";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ImageName", model.ImageName);
                        cmd.Parameters.AddWithValue("@ImagePath", model.ImagePath);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index");
            }

            // If ModelState is invalid, return the view with the model to show validation errors
            return View(model);
        }
    }
}
