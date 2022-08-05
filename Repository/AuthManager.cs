using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingAPI.VSCode.Contracts;
using HotelListingAPI.VSCode.Data;
using HotelListingAPI.VSCode.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HotelListingAPI.VSCode.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
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

        public async Task<IEnumerable<IdentityError>> RegisterAdmin(ApiUserDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user .UserName = userDto.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded){
                await _userManager.AddToRoleAsync(user, "Administrator");
            }

            return result.Errors;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            
            if(user == null || isValidUser == false)
            {
                return null;
            }

            var token = await GenerateToken(user);            

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id
            };
        }


        private async Task<string> GenerateToken(ApiUser user)
        {
            // take config for key
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            // sign credentials
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            // query database to query roles
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims).Union(roleClaims);

            // create the actual token with configuration including claims
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}