using ProyectoCoreLauraSanNicolas.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Services
{
    public interface IUserService
    {
        Task<String> Login(LoginVMO userloginVMO);
        public String GetToken();
        Task<String> SignUp(UserVMO userVmo);
        Task<UserVMO> GetUserById(int userId);
        Task DeleteUser(int userId);
        Task<int> GetUserIdByUsername(string username);
        Task<int> GetRoleId(int userId);
        Task EditUserInformation(String Name, String Surname, String Email, String Username);
        bool ValidatePassword(string password);
        bool ValidateEmail(string email);
    }
}
