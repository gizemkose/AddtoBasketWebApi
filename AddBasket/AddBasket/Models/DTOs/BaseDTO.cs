using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddBasket.Models.DTOs
{ 
    public class BaseDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

