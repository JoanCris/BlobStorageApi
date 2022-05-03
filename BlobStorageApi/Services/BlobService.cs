using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text;
using BlobStorageApi.Extensions;
using BlobStorageApi.Models;

namespace BlobStorageApi.Services
{
	public class BlobService : IBlobService
	{
		private readonly BlobContainerClient containerClient;

		public BlobService(BlobServiceClient blobServiceClient)
		{
			containerClient = blobServiceClient.GetBlobContainerClient("ficheros");
		}

		public async Task<BlobInf> getBlobAsync(string name)
		{
			var blobClient = containerClient.GetBlobClient(name);
			var blobDownloadInfo = await blobClient.DownloadStreamingAsync();
			var content = blobDownloadInfo.Value.Content;
			var contentType = blobDownloadInfo.Value.Details.ContentType;

			return new BlobInf(content, contentType);
		}

		public async Task<IEnumerable<string>> listBlobsAsync()
		{
			var blobs = containerClient.GetBlobsAsync();
			var items = new List<string>();

			await foreach (var blobItem in blobs)
			{
				items.Add(blobItem.Name);
			}

			return items;
		}

		public async Task uploadFileBlobAsync(string filePath, string fileName)
		{
			var blobClient = containerClient.GetBlobClient(fileName);

			await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = filePath.getContentType() });
		}

		public async Task uploadContentBlobAsync(string content, string fileName)
		{
			var blobClient = containerClient.GetBlobClient(fileName);
			var bytes = Encoding.UTF8.GetBytes(content);

			await using var memoryStream = new MemoryStream(bytes);

			await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = fileName.getContentType()});
		}

		public async Task deleteBlobAsync(string name)
		{
			var blobClient = containerClient.GetBlobClient(name);
			await blobClient.DeleteIfExistsAsync();
		}
	}
}
