using ProyectoCoreLauraSanNicolas.Models;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Mappings
{
    public class ProductMapper
    {
        public static ProductDTO MapToDTO(Product product)
        {
            ProductDTO productDTO = new ProductDTO();
            productDTO.Code = product.Code;
            productDTO.Id = product.Id;
            productDTO.Name = product.Name;
            productDTO.Type = product.Type;
            return productDTO;
        }

        public static Product MapToModel(ProductDTO productDto)
        {
            Product product = new Product();
            product.Code = productDto.Code;
            product.Id = productDto.Id;
            product.Name = productDto.Name;
            product.Type = productDto.Type;
            return product;
        }
    }
}
