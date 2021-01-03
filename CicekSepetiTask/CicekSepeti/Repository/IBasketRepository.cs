using CicekSepeti.Models;
using CicekSepeti.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
    public interface IBasketRepository
    {     
        void CreateorUpdateBasket(AddedItemDTO addedItem);
        List<BasketDTO> GetUserBasket(int userId);
    
    }
}
