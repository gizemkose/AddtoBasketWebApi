using CicekSepeti.Business.Services;
using CicekSepeti.Helpers;
using CicekSepeti.Models;
using CicekSepeti.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CicekSepeti.Controllers
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
            try// en son returnde yeni list dön, userın itemlerinin olduğu bir obje yarat
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
