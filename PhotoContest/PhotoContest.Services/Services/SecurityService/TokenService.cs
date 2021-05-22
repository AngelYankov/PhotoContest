using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhotoContest.Data;
using PhotoContest.Services.Contracts.SecurityContracts;
using PhotoContest.Services.ExceptionMessages;
using PhotoContest.Services.Models.SecurityModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services.SecurityService
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> userManager;
        private readonly JWT jwt;
        public TokenService(UserManager<User> userManager, IOptions<JWT> jwt)
        {
            this.userManager = userManager;
            this.jwt = jwt.Value;
        }
        /// <summary>
        /// Get token and user details.
        /// </summary>
        /// <param name="model">Validation model.</param>
        /// <returns>Returns details of user token.</returns>
        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                throw new ArgumentException(Exceptions.NotFoundEmail);
                //authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                //return authenticationModel;
            }
            if (await this.userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtTokenAsync(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await this.userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            throw new ArgumentException(Exceptions.IncorrectCredentials);
            //authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            //return authenticationModel;
        }
        /// <summary>
        /// Create JWT.
        /// </summary>
        /// <param name="user">User to create the token for.</param>
        /// <returns>Returns token.</returns>
        private async Task<JwtSecurityToken> CreateJwtTokenAsync(User user)
        {
            var userClaims = await this.userManager.GetClaimsAsync(user);
            var roles = await this.userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: this.jwt.Issuer,
                audience: this.jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(this.jwt.DurationInMinutes)),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
