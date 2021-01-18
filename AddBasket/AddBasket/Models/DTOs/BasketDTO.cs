using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBasket.Models.DTOs
{
    public class BasketDTO
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
    }
}
