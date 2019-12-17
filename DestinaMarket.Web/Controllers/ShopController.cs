using DestinaMarket.Services;
using DestinaMarket.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DestinaMarket.Web.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Checkout()
        {

            CheckoutViewModel model = new CheckoutViewModel();

            var  CartProductsCookie = Request.Cookies["CartProducts"];

            if (CartProductsCookie != null)
            {
                //var productIDs = CartProductsCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductsIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductsIDs);

            }

            return View(model);
        }
    }
}