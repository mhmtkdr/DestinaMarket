using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinaMarket.Entities
{
    public class Product : BaseEntity
    {
        public double Price { get; set; }

        public Category Category { get; set; }
        
    }
}
