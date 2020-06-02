using ProyectoCoreLauraSanNicolas.Models;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Mappings
{
    public class ResourceMapper
    {
        public static ResourceDTO MapToDTO(Resource resource)
        {
            ResourceDTO resourceDTO = new ResourceDTO();
            resourceDTO.Id = resource.Id;
            resourceDTO.Author = resource.Author;
            resourceDTO.Description = resource.Description;
            resourceDTO.Image = resource.Image;
            resourceDTO.Name = resource.Name;
            resourceDTO.Type = resource.Type;
            resourceDTO.Url = resource.Url;
            return resourceDTO;
        }

        public static Resource MapToModel(ResourceDTO resourceDto)
        {
            Resource resource = new Resource();
            resource.Id = resourceDto.Id;
            resource.Author = resourceDto.Author;
            resource.Description = resourceDto.Description;
            resource.Image = resourceDto.Image;
            resource.Name = resourceDto.Name;
            resource.Type = resourceDto.Type;
            resource.Url = resourceDto.Url;
            return resource;
        }
    }
}
