using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.PortableExecutable;

namespace OpenAIRequestExample
{
    public class FilePartitionService
    {
        private string connectionString = "";
        private string containerName = "";
        private string blobName = "pro-sandp500fund-inv-us.txt";

        public async Task PartitionFileAndSaveToBlob()
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(stream);
                var textContent = await ReadTextFileFromBlob(blobClient);
                var chunkSize = textContent.Length / 100; // Number of chunks = 100

                for (int i = 0; i < 100; i++)
                {
                    var startIndex = i * chunkSize - 500 < 0 ? 0 : i * chunkSize - 500;
                    var endIndex = startIndex + chunkSize + 500 > textContent.Length ? textContent.Length : startIndex + chunkSize + 500;

                    var chunkContent = textContent.Substring(startIndex, endIndex - startIndex);

                    var chunkFileName = $"chunk_{i}.txt"; 
                    await WriteTextFileToBlob(containerClient, chunkFileName, chunkContent);
                }
            }
        }

        async Task<string> ReadTextFileFromBlob(BlobClient blobClient)
        {
            var content = await blobClient.DownloadAsync();

            using (var reader = new StreamReader(content.Value.Content))
            {
                return await reader.ReadToEndAsync();
            }
        }

        async Task WriteTextFileToBlob(BlobContainerClient containerClient, string fileName, string content)
        {
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                await blobClient.UploadAsync(stream, true);
            }
        }
    }
}
