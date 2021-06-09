using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Web.Models.CategoryViewModels
{
    public class CategoriesViewModel
    {
        [MinLength(3, ErrorMessage = "Category name should be at least 3 characters long")]
        public string Name { get; set; }
        [Display(Name = "New name"), MinLength(3, ErrorMessage = "Category name should be at least 3 characters long")]
        public string NewName { get; set; }
    }
}
