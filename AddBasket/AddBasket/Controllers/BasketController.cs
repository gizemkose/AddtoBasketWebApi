using AddBasket.Business.Services;
using AddBasket.Helpers;
using AddBasket.Models;
using AddBasket.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AddBasket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public List<BasketDTO> Post([FromBody] AddedItemDTO addedItem)
        {
            try
            {
                var result = _basketService.AddItem(addedItem);

                return result;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }
    }
}
