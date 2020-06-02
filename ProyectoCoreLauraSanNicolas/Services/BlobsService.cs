using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Services
{
    public class BlobsService
    {
        public CloudBlobContainer GetContainer(String token)
        {
            CloudBlobContainer container = new CloudBlobContainer(new Uri(token));
            return container;
        }
        //public async Task<List<CloudBlockBlob>> GetBlobs(String token)
        //{
        //    CloudBlobContainer container = this.GetContainer(token);
        //    BlobContinuationToken conToken = null;

        //    BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(conToken);
        //    List<IListBlobItem> blobItemList = resultSegment.Results.ToList();

        //    List<CloudBlockBlob> blobList = new List<CloudBlockBlob>();
        //    foreach (CloudBlockBlob blobItem in blobItemList.Where(blob => blob is CloudBlockBlob))
        //    {
        //        //CloudBlockBlob selectedBlob = new CloudBlockBlob();
        //        //blobList.Add(blob.Name, blob.StorageUri.PrimaryUri.ToString());

        //    }
        //    return blobList;
        //}


        public String GetBlobContainerUri(String token)
        {
            return this.GetContainer(token).StorageUri.PrimaryUri.ToString();
        }
    }
}
