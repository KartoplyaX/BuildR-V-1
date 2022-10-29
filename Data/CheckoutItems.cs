using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildR_V_1.Data
{
    public class CheckoutItems
    {
        [Key, Required]
        public int ID { get; set; }
        [Required]
        public decimal Money { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
