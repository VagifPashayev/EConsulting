using System.ComponentModel.DataAnnotations;

namespace EConsult.Areas.Admin.ViewModels.Category;

public abstract class BaseCategoryViewModel
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Name { get; set; }
}
