using EConsult.Database.Models;
using EConsult.Services.Abstracts;

namespace EConsult.Services.Concretes
{
    public class ChatService : IChatService
    {
        private static IDictionary<string, string> _userConnectionIds;

        public ChatService()
        {
            _userConnectionIds = new Dictionary<string, string>();
        }

        public void AddConnectionId(string userId, string connectionId)
        {
            if (!_userConnectionIds.ContainsKey(userId))
            {
                _userConnectionIds.Add(userId, connectionId);
            }
        }

        public void RemoveConnectionId(string userId, string connectionId)
        {
            if (_userConnectionIds.ContainsKey(userId))
            {
                _userConnectionIds.Remove(userId);
            }
        }

        public string GetConnectionIds(string connectionId)
        {
            foreach (var kvp in _userConnectionIds)
            {
                if (kvp.Value == connectionId)
                {
                    return kvp.Key;
                }
            }

            return string.Empty;
        }
    }
}
