using ProyectoCoreLauraSanNicolas.Models;
using ProyectoCoreLauraSanNicolas.Models.ViewModels;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Mappings
{
    public class UserMapper
    {
        public static UserDTO MapToDTO(User user)
        {
            UserDTO userDto = new UserDTO();
            userDto.Id = user.Id;
            userDto.Name = user.Name;
            userDto.Surname = user.Surname;
            userDto.Email = user.Email;
            userDto.Username = user.Username;
            userDto.Password = user.Password;
            userDto.PasswordSalt = user.PasswordSalt;
            userDto.RoleId = user.RoleId;
            userDto.Activated = user.Activated;
            return userDto;
        }

        public static User MapToModel(UserDTO userDto)
        {
            User user = new User();
            user.Id = userDto.Id;
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Email = userDto.Email;
            user.Username = userDto.Username;
            //user.Password = userDto.Password;
            //user.PasswordSalt = userDto.PasswordSalt;
            //user.RoleId = userDto.RoleId;
            //user.Activated = userDto.Activated;
            return user;
        }
    }
}
