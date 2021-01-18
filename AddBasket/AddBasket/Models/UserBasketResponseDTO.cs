using AddBasket.Entities;
using AddBasket.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBasket.Models
{
    public class UserBasketResponseDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<BasketDTO> Items { get; set; }

    }
}
