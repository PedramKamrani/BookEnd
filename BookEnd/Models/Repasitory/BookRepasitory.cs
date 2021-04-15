using BookEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models.Repasitory
{
    public class BookRepasitory
    {
        private readonly BookContext _context;
        public BookRepasitory(BookContext context)
        {
            _context = context;
        }

        public List<TreeViewModel> GetAllCategory()
        {
            var Categores = (from c in _context.Categories
                             where c.PCategory == null
                             select
                             new TreeViewModel { CategoryID = c.CategoryID, CategoryName = c.CategoryName })
                             .ToList();
            foreach (var item in Categores)
            {
               BindSubCategory(item);
            }
            return Categores;
        }
        public void BindSubCategory(TreeViewModel Category)
        {
            var SubCategores = (from c in _context.Categories
                                where (c.PCategory == Category.CategoryID)
                                select 
                                (new TreeViewModel { CategoryID = c.CategoryID, CategoryName = c.CategoryName }))
                                .ToList();
         foreach(var item in SubCategores)
        {
                BindSubCategory(item);
                Category.SubCategores.Add(item);
        }

                
        }
    }
}
