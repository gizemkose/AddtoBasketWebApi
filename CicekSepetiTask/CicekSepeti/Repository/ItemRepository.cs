using CicekSepeti.Entities;
using CicekSepeti.Helpers;
using CicekSepeti.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
    public class ItemRepository:IItemRepository
    {
        private readonly ApiContext _context;
        public ItemRepository(ApiContext context)
        {
            _context = context;
        }
        public virtual bool StockCheck(AddedItemDTO addedItem)
        {
            var stock = _context.CompanyItem.Any(s => s.CompanyId == addedItem.Company.Id && s.ItemId == addedItem.Item.Id && s.Stock > 0);
            var list=_context.CompanyItem.ToList();
            if (stock)
                return true;
            else
                throw new AppException("Out of stock");     
        }
        public virtual void UpdateCompanyStock(AddedItemDTO addedItem)
        {
            var updatedCompanyItem = _context.CompanyItem.FirstOrDefault(s => s.CompanyId == addedItem.Company.Id && s.ItemId == addedItem.Item.Id);
            updatedCompanyItem.Stock -= 1;

            _context.CompanyItem.Update(updatedCompanyItem);
            _context.SaveChanges();

        }
    }
}
