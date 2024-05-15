using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Service.Interface.Admin
{
    public interface IAuthenticateService
    {
        Task<AuthResponseModel> Login(UserLoginDto user);
        ResponseModel Logout(int userId);
    }
}
