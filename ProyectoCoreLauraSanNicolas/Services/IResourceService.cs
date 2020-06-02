using PagedList;
using ProyectoCoreLauraSanNicolas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Services
{
    public interface IResourceService
    {
        Task<IPagedList<Resource>> GetResource(int T);
        Task<String> GetBlobToken();
    }
}
