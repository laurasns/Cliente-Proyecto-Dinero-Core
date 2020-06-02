using ProyectoCoreLauraSanNicolas.Mappings;
using ProyectoCoreLauraSanNicolas.Models;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoCoreLauraSanNicolas.Services
{
    public class ProductService : IProductService
    {
        ApiMethods request;

        public ProductService(ApiMethods request)
        {
            this.request = request;
        }

        public async Task<List<Product>> GetProducts(String type)
        {
            String url = "/api/Product/GetProducts/"+type;
            ApiResponse<List<ProductDTO>> data = await request.CallApiWithoutAuth<ApiResponse<List<ProductDTO>>>(url);
            if(data.Data != null)
            {
                IEnumerable<ProductDTO> enumerable = data.Data as IEnumerable<ProductDTO>;
                List<Product> selectedProducts = new List<Product>();
                foreach (ProductDTO prod in enumerable)
                {
                    Product product = ProductMapper.MapToModel(prod);
                    selectedProducts.Add(product);
                }
                return selectedProducts;
            } else
            {
                return null;
            }
            
        }

    }
}