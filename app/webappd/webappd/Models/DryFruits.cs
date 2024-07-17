﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webappd.Models
{
    public class DryFruits
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}