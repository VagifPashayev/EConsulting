using EConsult.Database.Models;

namespace EConsult.Services.Abstracts;

public interface IUserActivationService
{
    void CreateAndSendActivationToken(User user);
}
