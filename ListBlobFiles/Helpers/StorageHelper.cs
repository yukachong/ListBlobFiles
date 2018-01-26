﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListBlobFiles
{
    public class StorageHelper
    {

        public static async Task<List<string>> GetBlobFileList(string storageConnectionString, string containerName)
        {
            // Get Reference to Blob Container
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Fetch info about all files in the container
            BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(null);
            IEnumerable<IListBlobItem> blobItems = resultSegment.Results;

            // Extract the URI of the files into a new list
            List<string> fileUris = new List<string>();
            foreach (var blobItem in blobItems)
            {
                fileUris.Add(blobItem.StorageUri.PrimaryUri.ToString());
            }

            return fileUris;
        }

    }
}
