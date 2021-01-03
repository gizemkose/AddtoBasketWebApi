using CicekSepeti.Models;
using CicekSepeti.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Business.Services
{
   public interface IBasketService
    {
       List<BasketDTO> AddItem(AddedItemDTO addedItem);
    }
}
