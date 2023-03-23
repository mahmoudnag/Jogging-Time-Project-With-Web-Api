using jogging_times.Models;

namespace jogging_times.Services
{
    public interface IAuthService
    {

        Task<AuthModel> RegisterAsyncUser(RegisterModel model);
        Task<AuthModel> RegisterAsyncUserManager(RegisterModel model);
        Task<AuthModel> RegisterAsyncAdmin(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);

    }
}
