using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webappd.Models
{
    public class CombinedSpices
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}