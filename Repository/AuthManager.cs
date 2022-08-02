using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingAPI.VSCode.Contracts;
using HotelListingAPI.VSCode.Data;
using HotelListingAPI.VSCode.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.VSCode.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }
        
        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            // you can think about:
            // should the Email also be the Username
            // or should it be different?

            // for us the Email serves the purpose, since it should be unique
            var user = _mapper.Map<ApiUser>(userDto);
            user .UserName = userDto.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded){
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }
    }
}