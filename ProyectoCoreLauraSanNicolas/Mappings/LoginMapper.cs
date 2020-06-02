using ProyectoCoreLauraSanNicolas.Models.ViewModels;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Mappings
{
    public class LoginMapper
    {
        public static LoginDTO MapToDTO(LoginVMO login)
        {
            LoginDTO loginDTO = new LoginDTO();
            loginDTO.Username = login.Username;
            loginDTO.Password = login.Password;
            return loginDTO;
        }
        public static LoginVMO MapToModel(LoginDTO loginDto)
        {
            LoginVMO login = new LoginVMO();
            login.Username = loginDto.Username;
            login.Password = loginDto.Password;
            return login;
        }
    }
}
