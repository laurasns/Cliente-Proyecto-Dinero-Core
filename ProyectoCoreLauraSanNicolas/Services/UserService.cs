using ProyectoCoreLauraSanNicolas.Models;
using ProyectoCoreLauraSanNicolas.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProyectoCoreLauraSanNicolas.Mappings;
using ProyectoDineroNuGet.Models;
using System.Security.Claims;
using System.Threading;
using System.Security.Principal;

namespace ProyectoCoreLauraSanNicolas.Services
{
    public class UserService : IUserService
    {
        private const string ApiGetUserByUsername = "/api/User/GetUserByUsername/";
        private const string ApiGetUserById = "/api/User/GetUserById/";
        private const string ApiGetUserRoleById = "/api/User/GetUserRole/";
        private readonly ClaimsPrincipal principal;
        ApiMethods request;

        public UserService(ApiMethods request, IPrincipal principal)
        {
            this.request = request;
            this.principal = principal as ClaimsPrincipal;
        }

        public String GetToken()
        {
            ClaimsPrincipal userClaim = principal;
            String userToken = userClaim.Claims.Where(c => c.Type == "Token").Select(c => c.Value).FirstOrDefault();
            return userToken;
        }
        public async Task<String> Login(LoginVMO userloginVMO)
        {
            LoginDTO userLogin = LoginMapper.MapToDTO(userloginVMO);
            String token = await this.request.GetToken(userLogin);
            return token;
        }

        public async Task<String> SignUp(UserVMO userVmo)
        {
            UserDTO userDto = VMUserMapper.MapToDTO(userVmo);
            ApiResponse<UserDTO> response = await request.AddUser<ApiResponse<UserDTO>>(userDto);
            String result = response.ErrorMessage;
            if(result != null)
            {
                return result;
            }
            return "OK";
        }

        public async Task<UserVMO> GetUserById(int userId)
        {
            String userToken = GetToken();
            ApiResponse<UserDTO> data = await request.CallApiWithAuth<ApiResponse<UserDTO>>(ApiGetUserById + userId, userToken);
            UserDTO user = new UserDTO();
            user = data.Data;
            UserVMO userVMO = VMUserMapper.MapToModel(user);

            return userVMO;
        }

        public async Task<int> GetUserIdByUsername(string username)
        {
            String userToken = GetToken();
            UserDTO user = new UserDTO();
            ApiResponse<UserDTO> data = await request.CallApiWithAuth<ApiResponse<UserDTO>>(ApiGetUserByUsername + username, userToken);
            user = data.Data;
            UserVMO userVMO = VMUserMapper.MapToModel(user);
            return userVMO.Id;
        }

        public async Task<int> GetRoleId(int userId)
        {
            String userToken = GetToken();
            int role = await request.CallApiWithAuth<int>(ApiGetUserRoleById + userId, userToken);
            return role;
        }

        public async Task EditUserInformation(String Name, String Surname, String Email, String Username)
        {
            String userToken = GetToken();
            await request.EditUser(Name, Surname, Email, Username, userToken);
        }

        public async Task DeleteUser(int userId)
        {
            String userToken = GetToken();
            await request.DeleteUser(userId, userToken);
        }

        public bool ValidatePassword(string password)
        {
            const int minLength = 8;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;
            bool lengthRquirement = password.Length >= minLength;
            if (lengthRquirement)
            {
                foreach(char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }
            bool isOk = lengthRquirement
              && hasUpperCaseLetter
              && hasLowerCaseLetter
              && hasDecimalDigit;              
            return isOk;
        }

        public bool ValidateEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}