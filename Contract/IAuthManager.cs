using HotelListingAPI.VSCode.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HotelListingAPI.VSCode.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        Task<IEnumerable<IdentityError>> RegisterAdmin(ApiUserDto userDto);
        Task<AuthResponseDto> Login(LoginDto loginDto);
        //Task<string> CreateRefreshToken();
        // Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
