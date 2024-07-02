using ApplicationLayer.Commands.UserCommand;
using ApplicationLayer.Commons;
using ApplicationLayer.Contracts;
using DomainLayer.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationLayer.Handlers.UserHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUser _userRepo;
        private readonly JwtSetting _jwtSetting;

        public LoginCommandHandler(IUser userRepo, IOptionsMonitor<JwtSetting> optionsMonitor)
        {
            _userRepo = userRepo;
            _jwtSetting = optionsMonitor.CurrentValue;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetUserByUsernameAndPasswordAsync(request.Username, request.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSetting.SecretKey);
            var tokenDescriptions = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserName", user.UserName),
                    new Claim("Id", user.Id.ToString()),

                    new Claim("TokenId",Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptions);
            return tokenHandler.WriteToken(token);
        }
    }
}

