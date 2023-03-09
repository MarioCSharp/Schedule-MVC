using Schedule_MVC.Models.Admin;

namespace Schedule_MVC.Services.User
{
    public interface IUserService
    {
        string GetUserId();

        bool IsApproved(string id = null);

        bool IsAdmin(string id = null);

        List<UserDisplayModel> GetNonApproved();

    }
}
