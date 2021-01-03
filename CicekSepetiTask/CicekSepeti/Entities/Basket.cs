using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Entities
{
    public class Basket
    {
        public int Id { get; set; }//basketId, not pk
        public int ItemId { get; set; }
        public int ItemCount { get; set; }
        public int UserId { get; set; }
    }
}
