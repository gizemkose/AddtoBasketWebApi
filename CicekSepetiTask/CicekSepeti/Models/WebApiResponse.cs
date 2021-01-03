using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Models
{
    public class WebApiResponse
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }

        public UserBasketResponseDTO UserCurrentBasket { get; set; }
    }
}
