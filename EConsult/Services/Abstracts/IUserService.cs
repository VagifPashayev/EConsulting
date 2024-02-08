using EConsult.Contracts;
using EConsult.Database.Models;
using System.Security.Claims;

namespace EConsult.Services.Abstracts;

public interface IUserService
{
    public User CurrentUser { get; }

    string GetCurrentUserFullName();
    bool IsCurrentUserAuthenticated();
    List<Claim> GetClaimsAccordingToRole(User user);

    string GetUserFullName(User user);
    List<User> GetAllStaffMembers();
}
