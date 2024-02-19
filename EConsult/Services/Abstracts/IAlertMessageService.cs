using EConsult.Database.Models;

namespace EConsult.Services.Abstracts;

public interface IAlertMessageService
{
    void AddConnectionId(User user, string connectionId);
    void RemoveConnectionId(User user, string connectionId);
    List<string> GetConnectionIds(User user);
}
