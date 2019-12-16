using DestinaMarket.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinaMarket.Database
{
    public class DMContext : DbContext, IDisposable
    {
        public DMContext() : base("DestinaMarket")
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
