using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Models.DTOs
{
    public class AddedItemDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "UserId cannot be null.")]
        public int UserId { get; set; }
        public ItemDTO Item { get; set; }
        public int? ItemCount { get; set; }
        public CompanyDTO Company { get; set; }

    }
}
