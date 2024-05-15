using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Infrastructure.Interface.Admin;
using AppTemplate.Service.Interface.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Service.Implementation.Admin
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;            
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            return (UserDto)await _userRepository.GetAsync(x => x.Username == username);
        }
        public async Task<UserDto> GetByCredentialAsync(string username, string password)
        {
            return (UserDto)await _userRepository.GetAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<ResponseModelList> GetAll(int page, int size, int statusId)
        {
            return await _userRepository.GetAll(page, size, statusId);
        }
        public ApplicationUser GetCurrentUser()
        {
            //var user1 = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).Result;

            ApplicationUser user = new ApplicationUser { Id = 1, FullName = "Admin", Username = "admin", Password = "Admin@123", UserTypeId = 1 };
            return user;
        }
        public async Task<ResponseModel> AddUser(UserCreateVm model)
        {
            var loginId = GetCurrentUser().Id;
            return await _userRepository.AddUser(model, loginId);
        }
        public async Task<ResponseModel> UpdateUser(UserUpdateVm model, string token)
        {
            var loginId = GetCurrentUser().Id;
            return await _userRepository.UpdateUser(model, loginId, token);
        }
        public async Task<ResponseModel> ModifyStatus(int id, int statusId, string token)
        {
            var loginId = GetCurrentUser().Id;
            var res = await _userRepository.ModifyStatus(id, statusId, loginId, token);
            return res;
        }                
    }
}
