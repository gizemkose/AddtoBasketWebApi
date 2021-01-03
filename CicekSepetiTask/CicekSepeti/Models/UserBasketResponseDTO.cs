using CicekSepeti.Entities;
using CicekSepeti.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Models
{
    public class UserBasketResponseDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<BasketDTO> Items { get; set; }

    }
}
