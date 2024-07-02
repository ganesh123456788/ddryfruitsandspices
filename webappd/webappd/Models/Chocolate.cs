using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace webappd.Models
{
    public class Chocolate
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; } 
        public int Price {  get; set; }
    }
}