using DestinaMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestinaMarket.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }

        public List<int> CartProductsIDs { get; set; }
    }
}