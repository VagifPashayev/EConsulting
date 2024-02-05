using Org.BouncyCastle.Cms;

namespace EConsult.Contracts;

public class MessageDto
{
    public MessageDto() { }

    public MessageDto(string subject, string content, List<string> recipients)
    {
        Subject = subject;
        Content = content;
        Recipients = recipients;
    }

    public string Subject { get; set; }
    public string Content { get; set; }
    public List<string> Recipients { get; set; }
}
