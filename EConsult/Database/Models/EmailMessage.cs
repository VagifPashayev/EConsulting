using EConsult.Database.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EConsult.Database.Models;

public class EmailMessage : BaseEntity<int>, IAuditable
{
    public string Subject { get; set; }
    public string Content { get; set; }
    [NotMapped]
    public List<string> Receipents { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
