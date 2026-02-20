using System.ComponentModel.DataAnnotations;

namespace BeeProductApp.Models.Category
{
    public class CategoryPairVM
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string CategoryId { get; set; } = null!;
        public string Name { get; internal set; }
    }
}
