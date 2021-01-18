using AddBasket.Entities;
using AddBasket.Helpers;
using AddBasket.Models;
using AddBasket.Models.DTOs;
using AddBasket.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AddBasket.Business.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ApiContext _context;
        public BasketService(IBasketRepository basketRepository, IUserRepository userRepository, IItemRepository itemRepository, ApiContext context)
        {           
            _basketRepository = basketRepository;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
            _context = context;
        }

        public List<BasketDTO> AddItem(AddedItemDTO addedItem)
        {
            var trans = _context.Database.BeginTransaction();

            try
            {
                _userRepository.UserExist(addedItem.UserId);
                _itemRepository.StockCheck(addedItem);
                _basketRepository.CreateorUpdateBasket(addedItem);
                _itemRepository.UpdateCompanyStock(addedItem);

                trans.Commit();

                return _basketRepository.GetUserBasket(addedItem.UserId);
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new AppException(e.Message);
            }

        }

    }
}
