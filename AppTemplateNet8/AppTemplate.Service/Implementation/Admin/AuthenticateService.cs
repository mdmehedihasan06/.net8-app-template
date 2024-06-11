using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Helpers;
using AppTemplate.Infrastructure.Helper;
using AppTemplate.Infrastructure.Interface.Admin;
using AppTemplate.Service.Interface.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Service.Implementation.Admin
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenUtility _jwtTokenUtility;

        public AuthenticateService(IUserService userService, IPasswordHasher passwordHasher, IJwtTokenUtility jwtTokenUtility)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtTokenUtility = jwtTokenUtility;
        }

        public async Task<AuthResponseModel> Login(UserLoginDto user)
        {
            var userExists = (await _userService.GetByUsernameAsync(user.Username));
            if (userExists != null)
            {
                var passHash = _passwordHasher.HashPassword(user.Password, userExists.SecurityStamp);
                var checkUser = (await _userService.GetByCredentialAsync(user.Username, passHash));
                if (checkUser != null)
                {
                    // If valid, generate JWT token 
                    var (accessToken, refreshToken) = _jwtTokenUtility.GenerateTokens(checkUser.Id.ToString(), user.Username);
                    if (accessToken != null && refreshToken != null)
                    {
                        return new AuthResponseModel { Access_token = accessToken, Refresh_token = refreshToken, IsSuccess = true, Message = "Success", StatusCode = HttpStatusCode.OK };
                    }
                    else
                    {
                        return new AuthResponseModel { IsSuccess = false, Message = CommonMessages.Unauthenticated, StatusCode = HttpStatusCode.Unauthorized };
                    }
                }
                else
                {
                    return new AuthResponseModel { IsSuccess = false, Message = CommonMessages.Unauthenticated, StatusCode = HttpStatusCode.Unauthorized };
                }
            }
            return new AuthResponseModel { IsSuccess = false, Message = CommonMessages.DataDoesnotExists, StatusCode = HttpStatusCode.NotFound };
        }

        public ResponseModel Logout(int userId)
        {
            try
            {
                return Utilities.GetSuccessMsg("User logged out successfully.", null);
            }
            catch (Exception ex)
            {
                return Utilities.GetInternalServerErrorMsg(ex.Message);
            }

        }

        public Task<ResponseModel> ForgetPassword(ForgetPasswordDto forgetPassword)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> ChangePassword(ChangePasswordDto changePassword)
        {
            throw new NotImplementedException();
        }
        
        public Task<ResponseModel> UpdatePassword(UpdatePasswordDto updatePassword)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> RefreshToken(RefreshTokenDto refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
