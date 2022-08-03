using HotelListingAPI.VSCode.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HotelListingAPI.VSCode.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        Task<bool> Login(LoginDto loginDto);
    }
}
