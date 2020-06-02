using ProyectoCoreLauraSanNicolas.Models;
using ProyectoCoreLauraSanNicolas.Models.ViewModels;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Mappings
{
    public class VMUserMapper
    {
        public static UserDTO MapToDTO(UserVMO userVmo)
        {
            UserDTO userDto = new UserDTO();
            userDto.Id = userVmo.Id;
            userDto.Name = userVmo.Name;
            userDto.Surname = userVmo.Surname;
            userDto.Email = userVmo.Email;
            userDto.Username = userVmo.Username;
            userDto.Password = userVmo.Password;
            return userDto;
        }

        public static UserVMO MapToModel(UserDTO userDto)
        {
            UserVMO user = new UserVMO();
            user.Id = userDto.Id;
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Email = userDto.Email;
            user.Username = userDto.Username;
            return user;
        }
    }
}
