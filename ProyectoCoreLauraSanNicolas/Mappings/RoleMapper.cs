using ProyectoCoreLauraSanNicolas.Models;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Mappings
{
    public class RoleMapper
    {
        public static RoleDTO MapToDTO(Role role)
        {
            RoleDTO roleDTO = new RoleDTO();
            roleDTO.Id = role.Id;
            roleDTO.Name = role.Name;
            roleDTO.Users = role.Users.Select(user => UserMapper.MapToDTO(user)).ToList();
            return roleDTO;
        }

        public static Role MapToModel(RoleDTO roleDto)
        {
            Role role = new Role();
            role.Id = roleDto.Id;
            role.Name = roleDto.Name;
            role.Users = roleDto.Users.Select(userDto => UserMapper.MapToModel(userDto)).ToList();
            return role;
        }
    }
}
