using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Models.CategoryViewModels
{
    public class CategoriesViewModel
    {
        [MinLength(3)]
        public string Name { get; set; }
        [Display(Name = "New name"), MinLength(3)]
        public string NewName { get; set; }
    }
}
