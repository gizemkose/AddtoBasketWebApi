using CicekSepeti.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
   public interface IItemRepository
    {
        public bool StockCheck(AddedItemDTO addedItem);
        void UpdateCompanyStock(AddedItemDTO addedItem);
    }
}
