using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SITS_API.Application.Contracts;
using SITS_API.Application.DTOs;
using SITS_API.Domain.Entities;
using SITS_API.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SITS_API.Infrastructure.Repo
{
    internal class UserRepo : IUser
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public UserRepo(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUser = await FindUserByEmail(loginDTO.Email!);
            if (getUser == null) return new LoginResponse(false, "User not found, sorry");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
            if (checkPassword)
                return new LoginResponse(true, "Login successfully", GenerateJWTToken(getUser));
            else
                return new LoginResponse(false, "Invalid credentials");
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var getUser = await FindUserByEmail(registerUserDTO.Email!);
            if (getUser != null)
                return new RegistrationResponse(false, "User already exist");

            appDbContext.Users.Add(new ApplicationUser()
            {
                Title = registerUserDTO.Title,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                DateOfBirth = registerUserDTO.DateOfBirth,
                Gender = registerUserDTO.Gender,
                Email = registerUserDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password),
                Remark = registerUserDTO.Remark,
                DateCreated = DateTime.Now
            });
            await appDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Registration completed");
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) => await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        private string GenerateJWTToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName!),
                new Claim(ClaimTypes.Email, user.Email!),
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #region "User Details"

        public async Task<IEnumerable<ApplicationUser>> GetUserDetails()
        {
            return await appDbContext.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserDetailsById(int id)
        {
            return await appDbContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> UpdateUser(ApplicationUser user)
        {
            appDbContext.Entry(user).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
            return await appDbContext.Users.ToListAsync();
        }

        #endregion"

    }
}
