using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Entities
{
    public class CompanyItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
       public int CompanyId { get; set; }
       public int Stock { get; set; }
       public double Price { get; set; }



    }
}
