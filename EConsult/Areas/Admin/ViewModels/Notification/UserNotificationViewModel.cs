using System.Diagnostics.CodeAnalysis;

namespace EConsult.Areas.Admin.ViewModels.Notification
{
    public class UserNotificationViewModel
    {
        [NotNull]
        public List<EConsult.Database.Models.User> Users { get; set; }
        [NotNull]
        public string Title { get; set; }
        [NotNull]
        public string Content { get; set; }
        [NotNull]
        public List<int> SelectedUsersId { get; set; }
    }
}
