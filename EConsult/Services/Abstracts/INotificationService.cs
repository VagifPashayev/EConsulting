using EConsult.Database.Models;

namespace EConsult.Services.Abstracts
{
    public interface INotificationService
    {
        void SendOrderEmailNotification(Order order);
        public void SendPushNotification(User user, string title, string content);

    }
}
