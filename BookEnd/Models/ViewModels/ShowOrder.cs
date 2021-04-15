using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models.ViewModels
{
    public class ShowOrder
    {
        public int OrderDetailId { get; set; }
        public string ProductName { get; set; }
        public bool IsFainaly { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }
    }
}
