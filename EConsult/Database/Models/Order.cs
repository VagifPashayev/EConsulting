using EConsult.Contracts;
using EConsult.Database.Base;

namespace EConsult.Database.Models
{
    public class Order : BaseEntity<int>, IAuditable
    {
        public int ProductId { get; set; }
        public string LobbyCode { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
