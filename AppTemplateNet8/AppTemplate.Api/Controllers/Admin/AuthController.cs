using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Helpers;
using AppTemplate.Service.Interface.Admin;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace AppTemplate.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(IAuthenticateService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AppLogin")]
        public Task<AuthResponseModel> Login(UserLoginDto user)
        {
            var response = _authService.Login(user);
            return response;
        }

        [HttpPost]
        [Route("AppLogout")]
        public ResponseModel Logout()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _authService.Logout(userId);
            return response;
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public ResponseModel ForgetPassword()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _authService.Logout(userId);
            return response;
        }

        [HttpPost]
        [Route("ChangePassword")]
        public ResponseModel ChangePassword()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _authService.Logout(userId);
            return response;
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public ResponseModel UpdatePassword()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _authService.Logout(userId);
            return response;
        }

        [HttpPost]
        [Route("RefreshToken")]
        public ResponseModel RefreshToken()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _authService.Logout(userId);
            return response;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public ResponseModel Register()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = _authService.Logout(userId);
            return response;
        }
    }
}
