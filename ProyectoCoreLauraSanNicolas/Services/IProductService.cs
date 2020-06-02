using ProyectoCoreLauraSanNicolas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(String type);
    }
}
