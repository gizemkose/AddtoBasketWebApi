using AutoMapper;
using AddBasket.Entities;
using AddBasket.Models;
using AddBasket.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBasket.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public BasketRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }     

       public virtual void CreateorUpdateBasket(AddedItemDTO addedItem)
        {
            var userItem = CheckSameItemExists(addedItem);
            if (userItem == null)
            {
                CreateBasket(addedItem);
            }
            else
            {
                UpdateBasket(addedItem, userItem);
            }
        }
   
        private void CreateBasket(AddedItemDTO addedItem)
        {
            var newBasket = new Basket()
            {
                ItemId = addedItem.Item.Id,
                ItemCount = 1,
                UserId = addedItem.UserId
            };

            _context.Basket.Add(newBasket);
            _context.SaveChanges();
        }
        private void UpdateBasket(AddedItemDTO addedItem, Basket basket)
        {
            basket.ItemCount += 1;
            _context.Basket.Update(basket);
            _context.SaveChanges();
        }
        private Basket CheckSameItemExists(AddedItemDTO addedItem)
        {
            return _context.Basket.FirstOrDefault(s => s.UserId == addedItem.UserId && s.ItemId == addedItem.Item.Id);           
        }

        public virtual List<BasketDTO> GetUserBasket(int userId)
        {
           var basketDtoList= _context.Basket.Where(s => s.UserId == userId).ToList();
            return _mapper.Map<List<Basket>,List<BasketDTO>>(basketDtoList);
        }    
    }
}
