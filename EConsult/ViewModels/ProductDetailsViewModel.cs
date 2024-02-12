using EConsult.Database.Models;

namespace EConsult.ViewModels
{
    public class ProductDetailsViewModel
    {
        public List<User> Users { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; }
        public string ImageUrl { get; set; }
    }
}
