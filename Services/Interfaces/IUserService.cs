using W8_Backend.Models.UserModels.Input;
using W8_Backend.Models.UserModels.Output;

namespace W8_Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task RevokeTokenAsync(RevokeTokenRequest revokeTokenModel, HttpRequest Request);
        Task DeleteCookiesAsync(HttpResponse Response);
        Task<UserView> LoginAsync(AuthenticateRequest authenticateRequest, HttpResponse Response, HttpRequest request);
        Task<UserView> RefreshTokenAsync(RefreshTokenRequest model);

        Task<bool> CheckEmailAsync(GetByEmailRequest model);
        Task<UserView> GetUserByIdAsync(int userID);
        Task<UserView> CreateUserAsync(UserCreateRequest model, bool Init);
        Task<int> UpdateUserAsync(int userID, UserUpdateRequest model);
        Task<bool> IsTokenActiveAsync(CheckIfTokenIsExpiredRequest model);
    }
}
