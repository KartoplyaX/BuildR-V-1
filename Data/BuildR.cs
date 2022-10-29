using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BuildR_V_1.Data
{
    
    public class BuildR
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public decimal Money { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public Boolean Active { get; set; }
        public string ImageDescription { get; set; }
        public byte[] ImageData { get; set; }

    }
}
