using PagedList;
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
    public class ResourceService : IResourceService
    {
        ApiMethods request;

        public ResourceService(ApiMethods request)
        {
            this.request = request;
        }

        public async Task<IPagedList<Resource>> GetResource(int page)
        {
            string url = "/api/Resource/GetResources";
            ApiResponse<List<ResourceDTO>> data = await request.CallApiWithoutAuth<ApiResponse<List<ResourceDTO>>>(url);
            IEnumerable<ResourceDTO> enumerable = data.Data as IEnumerable<ResourceDTO>;
            List<Resource> resource = new List<Resource>();
            foreach(ResourceDTO re in enumerable)
            {
                Resource reso = ResourceMapper.MapToModel(re);
                resource.Add(reso);
            }
            IPagedList<Resource> resources = resource.ToPagedList(page, 4);
            return resources;
        }

        public async Task<String> GetBlobToken()
        {
            String token = await request.GetBlobToken();
            return token;
        }
    }
}