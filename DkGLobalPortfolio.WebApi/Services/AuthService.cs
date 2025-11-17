using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Models.User;
using DkGLobalPortfolio.WebApi.Models.User.Dto;
using DkGLobalPortfolio.WebApi.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class AuthService : Service<ApplicationUser>, IAuthService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _secretKey;
        public ApiResponse response;
        public AuthService(DkGlobalPortfolioDbContext db,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, string secretKey) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _secretKey = secretKey;
            response = new ApiResponse();
        }

        public async Task<ApiResponse> Login(LoginRequestDto requestDto)
        {
            var loginResponse = new LoginResponse();
            try
            {
                if (requestDto == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect";
                    return response;
                }
                var user = _db.ApplicationUsers?.FirstOrDefault(u => u.UserName.ToLower() == requestDto.Username.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect";
                    return response;
                }
                bool isValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);
                if (isValid == false)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect";
                    return response;
                }

                //geting user role
                var roles = await _userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);
                var tokenExpire = requestDto.RememberMe ? DateTime.UtcNow.AddDays(10) : DateTime.UtcNow.AddMinutes(30);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName.ToString()),
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault())

                        ]),
                    Expires = tokenExpire,
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };

                var token = tokenHandler.CreateToken(tokenDescription);

                loginResponse.UserId = user.Id;
                loginResponse.Role = roles.FirstOrDefault();
                loginResponse.Token = tokenHandler.WriteToken(token);
                loginResponse.TokenExpire = tokenExpire;

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Login Successful";
                response.Result = loginResponse;
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ApiResponse> Registration(CreateApplicationUserDto request)
        {
            try
            {
                ApplicationUser user = new()
                {
                    FullName = request.FullName,
                    UserName = request.FullName.ToLower(),
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                };

                var resultRes = await _userManager.CreateAsync(user, request.Password);

                if (resultRes.Succeeded)
                {
                    var roleAssigned = await _userManager.AddToRoleAsync(user, request.Role);
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "User created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = $"{string.Join("\n", resultRes.Errors.Select(s => s.Code))}\n{string.Join("\n", resultRes.Errors.Select(s => s.Description))}";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex?.Message + ex?.InnerException?.Message;
                return response;
            }
        }

        public void Update(ApplicationUser user)
        {
            _db.Update(user);
        }
    }
}
