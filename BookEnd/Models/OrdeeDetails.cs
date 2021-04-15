using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models
{
    public class OrdeeDetails
    {
        [Key]
        public int OrderDetails { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public Order Order { get; set; }
        public BookStor BookStor { get; set; }
    }
}
