using System.ComponentModel.DataAnnotations;

namespace EConsult.Areas.Admin.ViewModels.Product;

public class ProductAddViewModel
{
    [Required]
    public int[] UserIds { get; set; }
    public List<EConsult.Database.Models.User> Users { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int[] CategoryIds { get; set; }
    public List<Database.Models.Category> Categories { get; set; }

    public IFormFile Image { get; set; }
}
