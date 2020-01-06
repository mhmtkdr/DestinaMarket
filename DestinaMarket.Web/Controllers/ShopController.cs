using DestinaMarket.Entities;
using DestinaMarket.Services;
using DestinaMarket.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
//using System.Text;

namespace DestinaMarket.Web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.ShopPageSize();

            ShopViewModel model = new ShopViewModel();

            model.SearchTerm = searchTerm;
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.ShopPageSize();

            FilterProductsViewModel model = new FilterProductsViewModel();

            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }

        [Authorize]
        public ActionResult Checkout()
        {

            CheckoutViewModel model = new CheckoutViewModel();

            var  CartProductsCookie = Request.Cookies["CartProducts"];

            if (CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                model.CartProductsIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductsIDs);

                model.User = UserManager.FindById(User.Identity.GetUserId());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int ProductID)
        {
            ShopService.Instance.DeleteOrder(ProductID);
            return RedirectToAction("Checkout");
        }

        public JsonResult PlaceOrder(string productIDs)
        {
                JsonResult result = new JsonResult();
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                
                if (!string.IsNullOrEmpty(productIDs))
                {
                     var productQuantities = productIDs.Split('-').Select(x => int.Parse(x)).ToList();
                     
                     var boughtProducts = ProductsService.Instance.GetProducts(productQuantities.Distinct().ToList());
                     
                     Order newOrder = new Order();
                     newOrder.UserID = User.Identity.GetUserId();
                     newOrder.OrderedAt = DateTime.Now;
                     newOrder.Status = "Pending";
                     newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productID => productID == x.ID).Count());
                     
                     newOrder.OrderItems = new List<OrderItem>();
                     newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.ID, Quantity = productQuantities.Where(productID => productID == x.ID).Count() }));
                     
                     
                     var rowsEffected = ShopService.Instance.SaveOrder(newOrder);
                     
                     result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }

        public JsonResult SendMailToUser()
        {
            bool resulte = false;

            resulte = SendMail(User.Identity.Name, "DestinaMarket", "<p>Merhaba Değerli Müşterimiz,99223235 numaralı siparişiniz alınmıştır. Alışverişinde bizi tercih ettiğin için teşekkür ederiz. Siparişin kargoya verildiğinde seni bilgilendireceğiz.</p>");

            return Json(resulte, JsonRequestBehavior.AllowGet);
        }
        public bool SendMail(string toEmail,string subject,string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMassage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMassage.IsBodyHtml = true;

                client.Send(mailMassage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}