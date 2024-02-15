using EConsult.Database.Models;

namespace EConsult.Services.Abstracts
{
    public interface IChatService
    {
        void AddConnectionId(string userId, string connectionId);
        void RemoveConnectionId(string userId, string connectionId);
        string GetConnectionIds(string connectionId);
    }
}
