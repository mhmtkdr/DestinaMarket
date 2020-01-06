using DestinaMarket.Database;
using DestinaMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinaMarket.Services
{
    public class ShopService
    {
        #region Singleton
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();

                return instance;
            }
        }
        private static ShopService instance { get; set; }

        private ShopService()
        {
        }

        #endregion

        public int SaveOrder(Order order)
        {
            using (var context = new DMContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }

        public void DeleteOrder(int ProductID)
        {
            using (var context = new DMContext())
            {
                var orderItem = context.OrderItems.Find(ProductID);

                context.OrderItems.Remove(orderItem);
                context.SaveChanges();
            }
        }
    }
}
