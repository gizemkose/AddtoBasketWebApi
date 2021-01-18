using AddBasket.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBasket.Repository
{
   public interface IItemRepository
    {
        public bool StockCheck(AddedItemDTO addedItem);
        void UpdateCompanyStock(AddedItemDTO addedItem);
    }
}
