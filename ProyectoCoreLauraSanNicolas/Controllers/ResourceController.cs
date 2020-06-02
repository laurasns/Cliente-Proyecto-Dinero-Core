using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using PagedList;
using ProyectoCoreLauraSanNicolas.Models;
using ProyectoCoreLauraSanNicolas.Services;

namespace ProyectoCoreLauraSanNicolas.Controllers
{
    public class ResourceController : Controller
    {
        IResourceService resourceService;
        BlobsService blobsService;
        public ResourceController(IResourceService resourceService, BlobsService blobsService)
        {
            this.resourceService = resourceService;
            this.blobsService = blobsService;
        }
        // GET: Resource
        public async Task<IActionResult> Resources(int? page)
        {
            if (page == null || page == 0)
            {
                page = 1;
            }
            IPagedList<Resource> resources = await resourceService.GetResource(page.GetValueOrDefault());
            String token = await resourceService.GetBlobToken();
            String containerUri = this.blobsService.GetBlobContainerUri(token);

            ViewData["containerUri"] = containerUri;
            return View(resources);
        }
    }
}