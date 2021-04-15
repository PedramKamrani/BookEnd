using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models.ViewModels
{
   public class TreeViewModel
    {
        public TreeViewModel()
        {
            SubCategores = new List<TreeViewModel>();
        }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<TreeViewModel> SubCategores { get; set; }
    }
}
