using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Models.User;
using DkGLobalPortfolio.WebApi.Models.User.Dto;
using DkGLobalPortfolio.WebApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;

namespace DkGLobalPortfolio.WebApi.Controllers
{
    [Route("api/auth/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ApiResponse> LoginReq(LoginRequestDto req)
        {
            var response = await _serviceManager.Auth.Login(req);
            return response;
        }

        [HttpPost]
        [Route("registration")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Registration(CreateApplicationUserDto request)
        {
            var response = new ApiResponse();
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var isEmailValid = Regex.IsMatch(request.Email, pattern, RegexOptions.IgnoreCase);
            if (request == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Request can not be empty or null.";
                return response;
            }
            if (isEmailValid == false)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Email validation error";
                return response;
            }
            
            if (request.FullName == null || request.FullName == "" || request.Password == null || request.Password == "")
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "User name and password can not be empty or null.";
                return response;
            }
            var PasswordRegex = @"^(?=(.*[A-Z]))(?=(.*\d))(?=(.*\W))(?=.{6,})[A-Za-z\d\W]*$";
            var regex = new Regex(PasswordRegex);
            var validPassword = regex.IsMatch(request.Password);
            if (!validPassword)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Password must be at least 6 characters long, include at least one uppercase letter, one digit, and one non-alphanumeric character.";
                return response;
            }
            response = await _serviceManager.Auth.Registration(request);
            await _serviceManager.Save();

            return response;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllUserInfo(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<ApplicationUser>
                {
                    Expression = null,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var resultRes = await _serviceManager.Auth.GetAllAsync(genericReq);
                if (resultRes == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Not Found";
                    return response;
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Successful";
                response.Result = resultRes;
                return response;

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

        }

        [HttpGet]
        [Route("user/get")]
        public async Task<ApiResponse> GetUserInfo(string UserId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<ApplicationUser>
                {
                    Expression = x => x.Id == UserId,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var resultRes = await _serviceManager.Auth.GetAsync(genericReq);
                if (resultRes == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Not Found";
                    return response;
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Successful";
                response.Result = resultRes;
                return response;

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

        }

        [HttpPost]
        [Route("update-user-info")]
        public async Task<ApiResponse> UpdateUserInfo(UpdateApplicationUserDto req, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (req.UserId == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.Message = "Id Not Found";
                    return response;
                }
                var userData = await _serviceManager.Auth.GetAsync(new GenericServiceRequest<ApplicationUser>
                {
                    Expression = x => x.Id == req.UserId,
                    IncludeProperties = null,
                    NoTracking = false,
                    CancellationToken = cancellationToken
                });
                if (userData == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Success = false;
                    response.Message = "Data Not Found";
                    return response;
                }
                userData.FullName = req.FullName ?? userData.FullName;
                userData.PhoneNumber = req.PhoneNumber ?? userData.PhoneNumber;
                userData.Email = req.Email ?? userData.Email;
                await _serviceManager.Save();

                response.StatusCode = HttpStatusCode.NotFound;
                response.Success = false;
                response.Message = "Data Not Found";
                return response;

            }
            catch (Exception ex)
            {

                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.InnerException?.Message != null ? ex.InnerException.Message : ex.Message;
                return response;
            }

        }
    }
}
