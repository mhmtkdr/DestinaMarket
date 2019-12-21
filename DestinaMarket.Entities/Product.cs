using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinaMarket.Entities
{
    public class Product : BaseEntity
    {
        [Range(1, 100000)]
        public double Price { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public string ImageURL { get; set; }

    }
}
