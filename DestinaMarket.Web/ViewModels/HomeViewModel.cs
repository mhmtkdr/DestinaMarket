using DestinaMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestinaMarket.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}