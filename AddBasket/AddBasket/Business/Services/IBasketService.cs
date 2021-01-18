using AddBasket.Models;
using AddBasket.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBasket.Business.Services
{
   public interface IBasketService
    {
       List<BasketDTO> AddItem(AddedItemDTO addedItem);
    }
}
