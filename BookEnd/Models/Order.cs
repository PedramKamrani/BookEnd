using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int Sum { get; set; }
        public bool IsFainaly { get; set; }
        public List<OrdeeDetails> OrdeeDetails { get; set; }
    }

    
}
